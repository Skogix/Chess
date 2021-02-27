namespace Chess

open System.Collections.Generic
type Row = int
type Col = int
type Position = (Row * Col)
type Color = | White | Black
type PieceType =
  | Pawn
  | Bishop
  | Knight
  | Rook
  | Queen
  | King
type Command =
  | LogText of string
  | AddPiece of (Color * PieceType)
  | PrintHistory
  | GetHistory of AsyncReplyChannel<Command list>
type Piece = {
  Color: Color
  PieceType: PieceType
}
type Square =
  {
    Position: (int * int)
    Piece: Piece option
  }

module Helpers =
  let indexToCol i = ((i - 1) / 8) + 1
  let indexToRow i = ((i - 1) % 8) + 1
  let squareToIndex (row, col) = (8 * (row - 1) + (col - 1)) + 1
  let indexToSquare i = (indexToRow i, indexToCol i)
module BoardModule =
  type BoardObject () =
    let getInitPieces index =
      match (Helpers.indexToRow index, Helpers.indexToCol index) with
      | 2, _ -> Some { Color = White; PieceType = Pawn }
      | 7, _ -> Some { Color = Black; PieceType = Pawn }
      | 1, x ->
        match x with
        | 1 | 8 -> Some {Color = White; PieceType = Rook}
        | 2 | 7 -> Some {Color = White; PieceType = Knight}
        | 3 | 6 -> Some {Color = White; PieceType = Bishop}
        | 4     -> Some {Color = White; PieceType = Queen}
        | 5     -> Some {Color = White; PieceType = King}
        | _ -> failwith "inte en startpos"
      | 8, x ->
        match x with
        | 1 | 8 -> Some {Color = Black; PieceType = Rook}
        | 2 | 7 -> Some {Color = Black; PieceType = Knight}
        | 3 | 6 -> Some {Color = Black; PieceType = Bishop}
        | 4     -> Some {Color = Black; PieceType = Queen}
        | 5     -> Some {Color = Black; PieceType = King}
        | _ -> failwith "inte en startpos"
      | _, _ -> None
    let squares = [
                     for index in [1..64] do
                       {
                         Position = (Helpers.indexToCol index, Helpers.indexToRow index)
                         Piece = getInitPieces index
                       }
                     ]
    member this.Squares = squares
    member this.Square x = squares.[x]
  type State = {
    History: Command list
    ChessBoard: BoardObject
  }

module State =
  open BoardModule
  type Listener = State -> unit
  module State =
    type Agent() =
      let stateListeners = List<Listener>()
      let updateListeners (state:State) =
        for listener in stateListeners do
          listener state
      let initState: State = { History = []
                               ChessBoard = BoardObject() }
      let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
        let rec loop (oldState:State) = async {
          let board = oldState.ChessBoard
          let squares = board.Squares
          let! command = inbox.Receive()
          let newState =
            match command with
            | LogText msg ->
              {oldState with History = command :: oldState.History}
            | PrintHistory ->
              printfn "%A" oldState
              oldState
            | GetHistory channel ->
              channel.Reply(oldState.History)
              oldState
            | AddPiece (color, pieceType) ->
              let newPiece = Some {Color = color; PieceType = pieceType}
              let newPos: Position = (1,3)
              let newSquare: Square = { Position = newPos
                                        Piece = newPiece}
              let newBoard: Board = {}
              {oldState with ChessBoard = newBoard}
          updateListeners newState
          return! loop newState
        }
        loop initState
        )
      member this.SendCommand cmd = mailbox.Post cmd
      member private this.PostAndReply channel = mailbox.PostAndReply channel
      member this.GetHistory = mailbox.PostAndReply (fun replyChannel -> GetHistory(replyChannel))
      member this.State f = stateListeners.Add f
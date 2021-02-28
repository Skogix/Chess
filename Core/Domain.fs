module Chess

open System
module Helpers =
  let getColFromPosition pos = (pos / 10) % 10
  let getRowFromPosition pos = (pos / 1) % 10
type Color = | White | Black
type Row = int
type Col = int
type Position = int
type Notation = string
type PieceType =
  | Pawn
  | Bishop
  | Knight
  | Rook
  | Queen
  | King
type Piece = {
  Color: Color
  PieceType: PieceType
}
type Square = {
  Position: Position
  Piece: Piece option
} with
  member this.Col = Helpers.getColFromPosition this.Position
  member this.Row = Helpers.getRowFromPosition this.Position
  member this.Notation = 
    let col =
      match this.Col with
      | 1 -> 'A'
      | 2 -> 'B'
      | 3 -> 'C'
      | 4 -> 'D'
      | 5 -> 'E'
      | 6 -> 'F'
      | 7 -> 'G'
      | 8 -> 'H'
      | _ -> ' '
    let row =
      match this.Row with
      | 1 -> '1'
      | 2 -> '2'
      | 3 -> '3'
      | 4 -> '4'
      | 5 -> '5'
      | 6 -> '6'
      | 7 -> '7'
      | 8 -> '8'
      | _ -> ' '
    (col.ToString() + row.ToString())
type Board = {
  Squares: Square list
} with
  member this.Square x = this.Squares.[x]
module BoardModule =
  let boardPositions: Position list = 
      [81..88] @
      [71..78] @
      [61..68] @
      [51..58] @
      [41..48] @
      [31..38] @
      [21..28] @
      [11..18]
  let getInitPiece (pos:Position) =
    match (Helpers.getRowFromPosition pos, Helpers.getColFromPosition pos) with
    | _, 2 -> Some { Color = Black; PieceType = Pawn }
    | _, 7 -> Some { Color = White; PieceType = Pawn }
    | x, 1 ->
      match x with
      | 1 | 8 -> Some {Color = Black; PieceType = Rook}
      | 2 | 7 -> Some {Color = Black; PieceType = Knight}
      | 3 | 6 -> Some {Color = Black; PieceType = Bishop}
      | 4     -> Some {Color = Black; PieceType = Queen}
      | 5     -> Some {Color = Black; PieceType = King}
      | _ -> failwith "inte en startpos"
    | x, 8 ->
      match x with
      | 1 | 8 -> Some {Color = White; PieceType = Rook}
      | 2 | 7 -> Some {Color = White; PieceType = Knight}
      | 3 | 6 -> Some {Color = White; PieceType = Bishop}
      | 4     -> Some {Color = White; PieceType = Queen}
      | 5     -> Some {Color = White; PieceType = King}
      | _ -> failwith "inte en startpos"
    | _, _ -> None
  let initBoard =
    let squares = [for i in boardPositions do { Position = i
                                                Piece = getInitPiece i }]
    {Squares = squares}
  let emptyBoard =
    let squares = [for i in boardPositions do {Position = i
                                               Piece = None}]
    {Squares = squares}
module State =
  type Command =
    | PostText of string
    | GetBoard of AsyncReplyChannel<Board>
  type CommandAgent (init:Board) =
    let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
      let rec loop (oldBoard:Board) = async {
        let! command = inbox.Receive()
        match command with
        | PostText x -> printfn "%A" x
        | GetBoard channel ->
          channel.Reply oldBoard
//        printfn "::: FrånCommandAgent: %A" command
        return! loop oldBoard
      }
      loop init
      )
    member this.x = 4
    member this.SendCommand cmd = mailbox.Post cmd
    member this.GetBoard = mailbox.PostAndReply (fun channel -> GetBoard(channel)) 
    

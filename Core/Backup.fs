module Core.Backup
//open System
//module Helpers =
//  let getColFromPosition pos = (pos / 10) % 10
//  let getRowFromPosition pos = (pos / 1) % 10
//type Color = | White | Black
//type Id = int
//type Row = int
//type Col = int
//type Position = int
//type Notation = string
//type PieceType =
//  | Pawn
//  | Bishop
//  | Knight
//  | Rook
//  | Queen
//  | King
//type Square = {
//  Position: Position
//} with
//  member this.Col = Helpers.getColFromPosition this.Position
//  member this.Row = Helpers.getRowFromPosition this.Position
//  member this.Notation = 
//    let col =
//      match this.Col with
//      | 1 -> 'A'
//      | 2 -> 'B'
//      | 3 -> 'C'
//      | 4 -> 'D'
//      | 5 -> 'E'
//      | 6 -> 'F'
//      | 7 -> 'G'
//      | 8 -> 'H'
//      | _ -> ' '
//    let row =
//      match this.Row with
//      | 1 -> '1'
//      | 2 -> '2'
//      | 3 -> '3'
//      | 4 -> '4'
//      | 5 -> '5'
//      | 6 -> '6'
//      | 7 -> '7'
//      | 8 -> '8'
//      | _ -> ' '
//    (col.ToString() + row.ToString())
//type Piece = {
//  Id: Id
//  Color: Color
//  PieceType: PieceType
//  mutable Position: Position
//}
//type Pieces = Piece list
//type Board = {
//  Positions: Position list
//  Squares: Map<Position, Piece option>
//  Pieces: Pieces
//} with
//  member this.Square x = this.Squares.[x]
//type Move = {
//  Piece: Piece
//  From: Position
//  To: Position
//}
//module BoardModule =
//  let positions: Position list = 
//      [81..88] @
//      [71..78] @
//      [61..68] @
//      [51..58] @
//      [41..48] @
//      [31..38] @
//      [21..28] @
//      [11..18]
//  let emptySquareMap: Map<Position, Piece option> =
//    []
//    |> List.zip positions
//    |> Map.ofList
////  let initPieces =
////    // todo nästan klart
////  let getInitPiece (pos:Position) =
////    match (Helpers.getRowFromPosition pos, Helpers.getColFromPosition pos) with
////    | _, 2 -> Some { Color = Black; PieceType = Pawn }
////    | _, 7 -> Some { Color = White; PieceType = Pawn }
////    | x, 1 ->
////      match x with
////      | 1 | 8 -> Some {Color = Black; PieceType = Rook}
////      | 2 | 7 -> Some {Color = Black; PieceType = Knight}
////      | 3 | 6 -> Some {Color = Black; PieceType = Bishop}
////      | 4     -> Some {Color = Black; PieceType = Queen}
////      | 5     -> Some {Color = Black; PieceType = King}
////      | _ -> failwith "inte en startpos"
////    | x, 8 ->
////      match x with
////      | 1 | 8 -> Some {Color = White; PieceType = Rook}
////      | 2 | 7 -> Some {Color = White; PieceType = Knight}
////      | 3 | 6 -> Some {Color = White; PieceType = Bishop}
////      | 4     -> Some {Color = White; PieceType = Queen}
////      | 5     -> Some {Color = White; PieceType = King}
////      | _ -> failwith "inte en startpos"
////    | _, _ -> None
////  let initSquares: Square list =
////    [for pos in boardPositions do {Position = pos}]
////  let emptyBoard: Board =
////    let squares = initSquares
////    let pieces = []
////    { Squares = squares
////      Pieces = pieces }
////  let startingBoard: Board =
////    let squares = initSquares
////    let pieces = [
////      for i in [1..8] do { Color = White; PieceType = Pawn; Position = 20 + i}
////      for i in [1..8] do { Color = White; PieceType = Pawn; Position = 70 + i}
////    ]
////    { Squares = squares
////      Pieces = pieces }
//module State =
//  type Command =
//    | GetBoard of AsyncReplyChannel<Board>
//    | Move of Move
//  type CommandAgent (init:Board) =
//    let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
//      let rec loop (oldBoard:Board) = async {
//        let! command = inbox.Receive()
//        match command with
//        | GetBoard channel ->
//          channel.Reply oldBoard
//        return! loop oldBoard
//      }
//      loop init
//      )
//    member this.SendCommand cmd = mailbox.Post cmd
//    member this.PostAndReply<'T> command = mailbox.PostAndReply<'T> (fun channel -> command(channel))
//    member this.GetBoard = mailbox.PostAndReply (fun channel -> GetBoard(channel)) 
//    

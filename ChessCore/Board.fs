module ChessCore.Board

open System

type Rank = int
type File = int
type Color = | White | Black
type Item =
  | None
  | Pawn of Color
  | Bishop of Color
  | Knight of Color
  | Rook of Color
  | Queen of Color
  | King of Color
  | EnPassant
type Id = int
type Square = {
  Content: Item
  Id: int
}
type CastleRights =
  | WhiteQueenSide
  | WhiteKingSide
  | BlackQueenSide
  | BlackKingSide
type MoveCounter = uint
type Board = {
  Squares: Square list 
  SideToMove: Color
  CastleRights: CastleRights list
  EnPassant: File
  HalfMove: MoveCounter
  FullMove: MoveCounter
} with
  static member Notation id = Utility.notation id
let skogixNotation = "rnbqkbnr/pppppppp/......../......../......../......../PPPPPPPP/RNBQKBNR w KQkq - 0 1".Split ' '
let createBoard (input:string) =
  0
//  {
//    Squares = squares
//    SideToMove = SideToMove
//    CastleRights = failwith "todo"
//    EnPassant = failwith "todo"
//    HalfMove = failwith "todo"
//    FullMove = failwith "todo" }
//  
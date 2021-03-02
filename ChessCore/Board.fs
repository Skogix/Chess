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
let fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1".Split ' '
let createBoard (input:string) =
  let fen = input.Split ' '
  let Pieces = fen.[0] |> Seq.toList
  let SideToMove =
    match fen.[1] with
    | "w" -> White
    | "b" -> Black
    | _ -> failwith "fenfel"
  let CastleRights =
    [for char in fen.[2].ToCharArray() do
      match char with
      | 'k' -> BlackKingSide
      | 'q' -> BlackQueenSide
      | 'K' -> WhiteKingSide
      | 'Q' -> WhiteQueenSide
      | _ -> failwith "fenfel" ]
  let HalfMove = fen.[4]
  let FullMove = fen.[5]
  
  let squares =
    let charToPiece (c:char) (color:Color) =
      match Char.ToLower c with
      | 'p' -> Pawn color
      | 'r' -> Rook color
      | 'n' -> Knight color
      | 'b' -> Bishop color
      | 'q' -> Queen color
      | 'k' -> King color
      | _ -> failwith "fenfail"
    let empty x = [ for _ in 1..x do None ]
    [for char in Pieces do
       match char with
       | '1'|'2'|'3'|'4'|'5'|'6'|'7'|'8' -> empty (char |> Char.GetNumericValue |> int)
       | 'p'|'r'|'n'|'b'|'q'|'k' -> [charToPiece char Black]
       | 'P'|'R'|'N'|'B'|'Q'|'K' -> [charToPiece char White]
       | _ -> ()
    ] |> List.concat |> List.zip Utility.squareIds |> List.map (fun (x, y) -> {Id = x; Content = y})
  let EnPassant = fen.[3]
  squares
//  {
//    Squares = squares
//    SideToMove = SideToMove
//    CastleRights = failwith "todo"
//    EnPassant = failwith "todo"
//    HalfMove = failwith "todo"
//    FullMove = failwith "todo" }
//  
module ChessCore.Domain


type Rank = int
type File = int
type Position = File * Rank
type Notation = string
type Color = | White | Black
type HasMoved = | Yes | No
type Content =
  | Empty
  | Pawn of Color
  | Bishop of Color
  | Knight of Color
  | Rook of Color
  | Queen of Color
  | King of Color
  | EnPassant of Color
type System.Int32 with
  member this.File = this%10
  member this.Rank = this/10
type Id = int 
type Square = {
  Content: Content
  Id: Id
}
type Squares = Square list
type CastleRights =
  | WhiteQueenSide
  | WhiteKingSide
  | BlackQueenSide
  | BlackKingSide
type MoveCounter = int
let ( --> ) a b = a + " " + b
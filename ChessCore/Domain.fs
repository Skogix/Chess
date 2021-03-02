module ChessCore.Domain
type Direction = | Up | Down | Left | Right
type PieceAction =
  | Block
  | Capture
  | CaptureOrBlock
  | PawnCapture
type MoveType =
  | KnightMoves
  | RookMoves
  | KingMoves
  | BishopMoves
  | QueenMoves
  | PawnMoves
type Col = int
type Row = int
type Position = {Col:Col;Row:Row} with
  static member Create col row = {Col=col;Row=row}
let createPos (col:Col, row:Row) = {Col = col; Row = row}
let createPosFromList (input:(int*int) list) = [for (x,y) in input do {Col = x;Row = y}]
let createPosFromCols (cols:Col list, row:Row) = [for col in cols do {Row = row; Col = col}]
let createPosFromRows (col:Col, rows:Row list) = [for row in rows do {Row = row; Col = col}]
type Notation = string  
type PieceType =
  | Pawn
  | Bishop
  | Knight
  | Rook
  | Queen
  | King
type Color = | White | Black
type Piece = {
  Color: Color
  PieceType: PieceType
}
type Square = {
  Position: Position
  mutable Piece: Piece option
} with
  member this.Col = this.Position.Col
  member this.Row = this.Position.Row
  member this.Notation = Utility.notation this.Col this.Row
type Board = {
  Squares: Square array
  mutable HighlightedSquares: Square list
} with
  member this.ToList = this.Squares |> Array.toList
  member this.Square (col, row) = Array.find(fun index -> index.Position = {Row=row;Col=col}) this.Squares
  member this.Square (pos:Position) = Array.find(fun index -> index.Position = pos) this.Squares
  member this.AddPiece (color:Color) (pieceType:PieceType) (col, row) =
    this.Square(col,row).Piece <- Some {Color = color; PieceType = pieceType}
  member this.GetSquares (positions:Position list) = [for pos in positions do this.Square pos]

module Chess


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
  member this.HasPiece = this.Piece.IsSome
  member this.IsEmpty = this.Piece.IsNone
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
  Squares: Square array
  mutable HighlightedSquares: Square list
} with
  member this.ToList = this.Squares |> Array.toList
  member this.Square (col, row) = Array.find(fun index -> index.Position = {Row=row;Col=col}) this.Squares
  member this.Square (pos:Position) = Array.find(fun index -> index.Position = pos) this.Squares
  member this.AddPiece (color:Color) (pieceType:PieceType) (col, row) =
    this.Square(col,row).Piece <- Some {Color = color; PieceType = pieceType}
  member this.GetSquares (positions:Position list) = [for pos in positions do this.Square pos]
type Direction = | Up | Down | Left | Right
module Init =
  let getCol index = (index/1)%10
  let getRow index = (index/10)%10
  let positions: Position list =
    [for col in [1..8] do {Col=col;Row=8}] @
    [for col in [1..8] do {Col=col;Row=7}] @
    [for col in [1..8] do {Col=col;Row=6}] @
    [for col in [1..8] do {Col=col;Row=5}] @
    [for col in [1..8] do {Col=col;Row=4}] @
    [for col in [1..8] do {Col=col;Row=3}] @
    [for col in [1..8] do {Col=col;Row=2}] @
    [for col in [1..8] do {Col=col;Row=1}]
  let getInitPiece (pos:Position) =
    match (pos.Col, pos.Row) with
    | _, 2 -> Some { Color = White; PieceType = Pawn }
    | _, 7 -> Some { Color = Black; PieceType = Pawn }
    | x, 8 ->
      match x with
      | 1 | 8 -> Some {Color = Black; PieceType = Rook}
      | 2 | 7 -> Some {Color = Black; PieceType = Knight}
      | 3 | 6 -> Some {Color = Black; PieceType = Bishop}
      | 4     -> Some {Color = Black; PieceType = Queen}
      | 5     -> Some {Color = Black; PieceType = King}
      | _ -> failwith "inte en startpos"
    | x, 1 ->
      match x with
      | 1 | 8 -> Some {Color = White; PieceType = Rook}
      | 2 | 7 -> Some {Color = White; PieceType = Knight}
      | 3 | 6 -> Some {Color = White; PieceType = Bishop}
      | 4     -> Some {Color = White; PieceType = Queen}
      | 5     -> Some {Color = White; PieceType = King}
      | _ -> failwith "inte en startpos"
    | _, _ -> None
  let initSquares =
    [|
      for pos in positions do
        { Position = pos
          Piece = getInitPiece pos }
    |]
  let initBoard: Board = {
    Squares = initSquares
    HighlightedSquares = [] }
  let emptyBoard: Board = { Squares = [|for pos in positions do {Position = pos; Piece = None}|]
                            HighlightedSquares = [] }
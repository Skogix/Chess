module Chess

open System
open System.Drawing
open System.Drawing
open System.Net.Mime

type Col = int
type Row = int
type Position = {Col:Col;Row:Row} with
  static member Create col row = {Col=col;Row=row}
let createPos (col:Col, row:Row) = {Col = col; Row = row}
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
  member this.Square (col:Col, row:Row) = Array.find(fun index -> index.Position = {Row=row;Col=col}) this.Squares
  member this.Square (pos:Position) = Array.find(fun index -> index.Position = pos) this.Squares
  member this.AddPiece (color:Color) (pieceType:PieceType) (col, row) =
    this.Square(col,row).Piece <- Some {Color = color; PieceType = pieceType}
  member this.GetSquares (positions:Position list) = [for pos in positions do this.Square pos]
type MoveType =
  | Straight
  | Diagonal
  | Horse
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
let posToCollision (inputSquare:Square) (board:Board) =
  let pos = inputSquare.Position
  let isEnemyPiece piece = piece.Color <> inputSquare.Piece.Value.Color
  let distance (dir:Direction) =
    match dir with
    | Up -> abs (pos.Row - 8)
    | Down -> pos.Row - 1
    | Left -> pos.Col - 1
    | Right -> abs (pos.Col - 8)
  let straightMoves =
    [
      [for i in [1 .. distance Up]    do {Col=pos.Col;Row=pos.Row+i}]
      [for i in [1 .. distance Down]  do {Col=pos.Col;Row=pos.Row-i}]
      [for i in [1 .. distance Left]  do {Col=pos.Col-i;Row=pos.Row}]
      [for i in [1 .. distance Right] do {Col=pos.Col+i;Row=pos.Row}]
    ] |> List.map board.GetSquares
  let diagonalMoves =
    [
     [for i in [1..(min (distance Up) (distance Right))]   do {Col=pos.Col+i;Row=pos.Row+i}]
     [for i in [1..(min (distance Up) (distance Left))]    do {Col=pos.Col-i;Row=pos.Row+i}]
     [for i in [1..(min (distance Down) (distance Right))] do {Col=pos.Col+i;Row=pos.Row-i}]
     [for i in [1..(min (distance Down) (distance Left))]  do {Col=pos.Col-i;Row=pos.Row-i}]
    ] |> List.map board.GetSquares
  let horseMoves =
    let checkPos col row = col <= 8 && col >= 1 && row <= 8 && row >= 1
    let posList =
        let row = pos.Row
        let col = pos.Col
        [
          (col-1,row+2);(col+1,row+2) // up
          (col-1,row-2);(col+1,row-2) // down
          (col-2,row-1);(col-2,row+1) // left
          (col+2,row-1);(col+2,row+1) // left
//          {Col=pos.Col-1;Row=pos.Row+2};{Col=pos.Col+1;Row=pos.Row+2} // up
//          {Col=pos.Col-1;Row=pos.Row-2};{Col=pos.Col+1;Row=pos.Row-2} // down
//          {Col=pos.Col+2;Row=pos.Row+1};{Col=pos.Col+2;Row=pos.Row-1} // right
//          {Col=pos.Col-2;Row=pos.Row+1};{Col=pos.Col-2;Row=pos.Row+1} // left
        ] 
    [
      for col, row in posList do
        printfn "%A %A" col row
        if (checkPos col row) then [createPos(col, row)]
        else []
    ] |> List.map board.GetSquares
  let rec loop (rest:Square list) (out:Square list): Square list =
    match rest with
    | [] -> out
    | square::rest ->
      match square.Col, square.Row with
      | _ ->
        match square.Piece with
        | Some p ->
          match isEnemyPiece p with
          | true -> (square::out)
          | false -> out
        | None -> loop rest (square::out)
  let getMoves (move:MoveType): Square list =
    match move with
      | Straight -> straightMoves |> List.map (fun move -> loop move []) |> List.concat
      | Diagonal -> diagonalMoves |> List.map (fun move -> loop move []) |> List.concat
      | Horse -> horseMoves |> List.map (fun move -> loop move []) |> List.concat
      
  // todo: getmoves beroende på piecetype
  match inputSquare.Piece.Value.PieceType with
  | Queen -> getMoves Straight @ getMoves Diagonal
  | Rook -> getMoves Straight
  | Bishop -> getMoves Diagonal
  | Knight -> getMoves Horse
  | _ -> List.empty
module Chess


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
module Moves =
  type Move = {
    Piece: Piece
    From: Square
    To: Square
  }
  type Direction = | Up | Down | Left | Right
  let getSquareMoves (inputSquare:Square) (board:Board) =
    let inputPos = inputSquare.Position
    let distanceToEdge (dir:Direction) =
      match dir with
      | Up -> abs (inputPos.Row - 8)
      | Down -> abs (inputPos.Row - 1)
      | Left -> abs (inputPos.Col - 1)
      | Right -> abs (inputPos.Col - 8)
    let inputPiece = inputSquare.Piece.Value
    let col = inputPos.Col
    let row = inputPos.Row
    let posInsideBoard col row = col <= 8 && col >= 1 && row <= 8 && row >= 1
    let filterInsideBoard = List.filter (fun (col:Col, row:Row) -> posInsideBoard col row)
    let toPositions (list:(Col*Row) list) = [for (col,row) in (list |> filterInsideBoard) do {Col=col;Row=row}       ]
    let toSquares input = [for pos in input do board.Square(pos.Col, pos.Row)]
    let ifPiece (action:PieceAction) (list:Square list) =
      let isFriendly color = color <> inputPiece.Color
      let rec loop (rest:Square list) (out:Square list): Square list =
        match rest with
        | [] -> out
        | x::rest ->
          match x.Piece, action with
          | None, PawnCapture -> out
          | Some p, _ ->
            match action, isFriendly p.Color with
            | CaptureOrBlock, true -> (x::out)
            | CaptureOrBlock, false -> out
            | Block, true -> out
            | Capture, true -> (x::out)
            | PawnCapture, true -> (x::out)
            | _, _ -> out
          | None, _ -> loop rest (x::out)
      loop list []
    let pawnMoves =
      let homeRowMoves =
        match inputPiece.Color, row with
        | White, 2 -> [(col,row+1); (col,row+2)] |> toPositions |> toSquares |> ifPiece Block
        | Black, 6 -> [(col,row-1); (col,row-2)] |> toPositions |> toSquares |> ifPiece Block
        | White, _ -> [(col,row+1)] |> toPositions |> toSquares |> ifPiece Block
        | Black, _ -> [(col,row-1)] |> toPositions |> toSquares |> ifPiece Block
        | _, _ -> []
      let captureMoves =
        match inputPiece.Color with
        | White ->
          [[(col+1,row+1)] |> toPositions |> toSquares |> ifPiece PawnCapture ] @
          [[(col-1,row+1)] |> toPositions |> toSquares |> ifPiece PawnCapture ] |> List.concat
        | Black ->
          [[(col+1,row-1)] |> toPositions |> toSquares |> ifPiece PawnCapture ] @
          [[(col-1,row-1)] |> toPositions |> toSquares |> ifPiece PawnCapture ] |> List.concat
      homeRowMoves @ captureMoves
    let diagonalMoves =
      let toEdge =
          [
            [for i in [1..min (distanceToEdge Up) (distanceToEdge Left)] do (col-i,row+i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
            [for i in [1..min (distanceToEdge Up) (distanceToEdge Right)] do (col+i,row+i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
            [for i in [1..min (distanceToEdge Down) (distanceToEdge Left)] do (col-i,row-i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
            [for i in [1..min (distanceToEdge Down) (distanceToEdge Right)] do (col+i,row-i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
          ] |> List.concat
      toEdge 
    let kingMoves =
      let runCheck input =
        input
        |> List.filter (fun (col:Col, row:Row) -> posInsideBoard col row)
        |> toPositions
        |> toSquares
        |> ifPiece CaptureOrBlock
      let around =
        [
          [(col+1,row+1)] |> runCheck
          [(col+1,row)]   |> runCheck
          [(col+1,row-1)] |> runCheck
          [(col-1,row+1)] |> runCheck
          [(col-1,row)]   |> runCheck
          [(col-1,row-1)] |> runCheck
          [(col,row+1)]   |> runCheck
          [(col,row-1)]   |> runCheck
        ] |> List.concat
      around
    let straightMoves =
      let toEdge =
          [
            [for i in [1..distanceToEdge Up] do (col,row+i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
            [for i in [1..distanceToEdge Down] do (col,row-i)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock 
            [for i in [1..distanceToEdge Right] do (col+i,row)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
            [for i in [1..distanceToEdge Left] do (col-i,row)] |> toPositions |> toSquares |> ifPiece CaptureOrBlock
          ] |> List.concat
      toEdge
    let knightMoves =
      let runCheck x =
        x
        |> List.filter (fun (col:Col, row:Row) -> posInsideBoard col row)
        |> toPositions
        |> toSquares
        |> ifPiece CaptureOrBlock
      let posList =
        [ // todo; kolla varfor inte iter / map funkar som jag tror
          [(col-1,row+2)] |> runCheck
          [(col+1,row+2)] |> runCheck
          [(col-1,row-2)] |> runCheck
          [(col+1,row-2)] |> runCheck
          [(col-2,row-1)] |> runCheck
          [(col-2,row+1)] |> runCheck
          [(col+2,row-1)] |> runCheck
          [(col+2,row+1)] |> runCheck
        ] |> List.concat
      posList
      
    let output =
      match inputSquare.Piece with
      | None -> []
      | Some p ->
        match p.PieceType with
        | Queen -> straightMoves @ diagonalMoves 
        | Bishop -> diagonalMoves 
        | Knight -> knightMoves 
        | King -> kingMoves 
        | Rook -> straightMoves
        | Pawn -> pawnMoves
        | _ -> []
    output
module Chess

open System.Xml

type Id = int
type Col = int
type Row = int
type Position = int
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
let getRow index = (index/10)%10
let getCol index = (index/1)%10
type Square = {
  Position: Position
  mutable Piece: Piece option
} with
  member this.Col = getCol this.Position
  member this.Row = getRow this.Position
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
} with
  member this.ToList = this.Squares |> Array.toList
  member this.Square x = Array.find(fun index -> index.Position = x) this.Squares
  member this.Move x y =
    (this.Square y).Piece <- (this.Square x).Piece
    (this.Square x).Piece <- None
module Init =
  let getCol index = (index/1)%10
  let getRow index = (index/10)%10
  let positions: Position list = 
    [81..88] @
    [71..78] @
    [61..68] @
    [51..58] @
    [41..48] @
    [31..38] @
    [21..28] @
    [11..18]
  let getInitPiece (pos:Position) =
    match (getCol pos, getRow pos) with
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
  }
  let emptyBoard: Board = {Squares = [|for pos in positions do {Position = pos; Piece = None}|]}
module Rules =
  type Move = {
    Piece: Piece
    From: Square
    To: Square
  }
  let AddSquaresToEmptyBoard (squares:Square list) =
    let output = Init.emptyBoard
    for pieceToAdd in squares do
      (output.Square pieceToAdd.Position).Piece <- pieceToAdd.Piece
    output 
  
    
    
//  member this.Square x = Array.find(fun index -> index.Position = x) this.Squares
  let SquareListToBoard (squares:Square list): Board =
    let newArray = squares |> List.toArray
    {Squares = newArray}
  let AllPosInRow (pos:Position): Position list = [ for x in [1..8] do ((getRow pos)*10+x)]
  let AllPosInCol (pos:Position): Position list = [ for x in [1..8] do ((getCol pos)+x*10)]
  let GetAllSquaresInList (board:Board)(positions:Position list) =
    [ for pos in positions do
        board.Square pos ]
      
  
  let Rook (pos:Position) (board:Board) =
    0
  let GetMoves (pos:Position) (board:Board)  =
    match (board.Square pos).Piece with
    | Some piece ->
      match piece.PieceType with
      | Rook -> [31]
      | _ -> []
    | None -> []
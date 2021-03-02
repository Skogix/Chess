module ChessCore.Init
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

// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.Data
open Chess
open Scratch.Extra
type Move = {
  Piece: Piece
  From: Square
  To: Square
}
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
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  let     a = 5
  let     b = 1
  let piece = Pawn
  let color = White
  testBoard.AddPiece color piece (a, b)
  testBoard.AddPiece Black Knight (4,2)
  testBoard.AddPiece White Queen (6,2)
  testBoard.AddPiece White Bishop (1,2)
  let testSquare = testBoard.Square (a, b)
  printfn "%A" (getSquareMoves testSquare testBoard)
  testBoard.HighlightedSquares <- getSquareMoves testSquare testBoard
  let printAllSquaresAttacking testBoard =
    let huhu =
      [for square in testBoard.Squares do
         if square.HasPiece then yield getSquareMoves square testBoard
         ]
    let highlight =
      huhu |> List.concat
    testBoard.HighlightedSquares <- highlight
  printBoard testBoard
  
  0 // return an integer exit code
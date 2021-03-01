// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Scratch.Extra
module Rules =
  type Move = {
    Piece: Piece
    From: Square
    To: Square
  }
let posToCollision (square:Square) (board:Board) =
  let startingCol = square.Col
  let startingRow = square.Row
  let hasPiece (square:Square) = square.Piece.IsSome
  let isEnemyPiece (piece:Piece) = piece.Color <> square.Piece.Value.Color
  let getPosUp: Square list =
    [
      for row in [startingRow%10+1..8] do
        board.Square(startingCol, row)
    ]
  let getPosRight: Square list =
    [
      for col in [startingCol%10+1..8] do
        board.Square(col, startingRow)
    ]
  let rec loop (rest:Square list) (out:Square list)  =
    match rest with
    | [] -> out
    | x::rest ->
      match x.Piece with
      | Some piece ->
        match isEnemyPiece piece with
        | true -> (x::out)
        | false -> out
      | None -> loop rest (x::out)
//      if (hasPiece x) then out
//      else loop rest (x::out) 
//  loop getPosUp []
  loop getPosRight [] @ loop getPosUp []
    
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  testBoard.AddPiece White Rook (2,2)
  let rookSquare = testBoard.Square (2,2)
  testBoard.AddPiece Black Pawn (2,5)
  testBoard.AddPiece White Pawn (5,2)
  testBoard.HighlightedSquares <- (posToCollision rookSquare testBoard)
  printBoard testBoard
  0 // return an integer exit code
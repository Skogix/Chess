// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Scratch.Extra
type Move = {
  Piece: Piece
  From: Square
  To: Square
}
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  testBoard.AddPiece White Knight (3,3)
  let testSquare = testBoard.Square (3,3)
  testBoard.AddPiece Black Pawn (2,5)
  testBoard.AddPiece Black Pawn (5,6)
  testBoard.AddPiece White Pawn (5,3)
  testBoard.HighlightedSquares <- (posToCollision testSquare testBoard)
  printBoard testBoard
  0 // return an integer exit code
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
let getSquaresLeft (square:Square) (board:Board) =
  []
let checkCollisionFrom (square:Square) (board:Board): Position list =
  let squaresLeft = getSquaresLeft square board
  [for square in squaresLeft do square.Position]
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  (testBoard.Square (4,2)).Piece <- Some { Color = White
                                           PieceType = Rook }
  (testBoard.Square (4,6)).Piece <- Some { Color = Black
                                           PieceType = Pawn }
  let highlights = checkCollisionFrom (testBoard.Square (4,2)) testBoard
//  testBoard.HighlightedSquares <- highlights
  testBoard.HighlightedSquares <- [createPos(1,2)]
  printBoard testBoard
  0 // return an integer exit code
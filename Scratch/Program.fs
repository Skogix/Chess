// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System.Data
open Chess
open Scratch.Extra
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
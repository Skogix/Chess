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
  let pos = square.Position
  let isEnemyPiece (piece:Piece) = piece.Color <> square.Piece.Value.Color
  let up =    [pos.Row%10+1 .. 8]
  let right = [pos.Col%10+1 .. 8]
  let down =  [1 .. pos.Row%10-1]
  let left =  [1 .. pos.Col%10-1]
  let getPosUp = [ for row in up do board.Square(square.Col, row) ]
  let getPosDown = [ for row in down do board.Square(square.Col, row) ]
  let getPosRight = [ for col in right do board.Square(col, square.Row) ]
  let getPosLeft = [ for col in left do board.Square(col, square.Row) ]
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
  let straightMoves = loop getPosRight [] @ loop getPosLeft [] @ loop getPosDown [] @ loop getPosUp []
  straightMoves
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
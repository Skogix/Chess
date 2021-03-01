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
  let up =    [for row in [pos.Row%10+1 .. 8] do createPos(pos.Col, row)]
  let right = [ for col in [pos.Col%10+1 .. 8] do createPos(col, pos.Row)]
  let upright = [
    let smallest =
      min up.Length right.Length
    for x in [1..smallest] do
        createPos(pos.Col+x, pos.Row+x)
  ]
  let down =  [1 .. pos.Row%10-1]
  let left =  [1 .. pos.Col%10-1]
  let getPosUp = [
    for p in up do
      board.Square(p.Col, p.Row)
  ]
  let getPosRight = [
    for p in right do
      board.Square(p.Col, p.Row)
  ]
  let getPosUpRight = [
    for pos in upright do board.Square(pos.Col, pos.Row)
  ]
  let getPosDown = [ for row in down do board.Square(square.Col, row) ]
  let getPosLeft = [ for col in left do board.Square(col, square.Row) ]
  let rec loop (rest:Square list) (out:Square list)  =
    match rest with
    | [] -> out
    | x::rest ->
      match x.Col, x.Row with
      | _, (1 | 8) -> (x::out)
      | (1 | 8), _ -> (x::out)
      | _ -> 
        match x.Piece with
        | Some piece ->
          match isEnemyPiece piece with
          | true -> (x::out)
          | false -> out
        | None -> loop rest (x::out)
//  let straightMoves = loop getPosRight [] @ loop getPosLeft [] @ loop getPosDown [] @ loop getPosUp []
  printfn "%A" upright
  loop getPosUpRight []
//  printfn "%A" (loop getPosUpRight [])
//  straightMoves
//  loop getPosUp [] @ loop getPosRight []
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  testBoard.AddPiece White Rook (2,3)
  let rookSquare = testBoard.Square (2,3)
  testBoard.AddPiece Black Pawn (2,5)
  testBoard.AddPiece White Pawn (5,2)
  testBoard.HighlightedSquares <- (posToCollision rookSquare testBoard)
  printBoard testBoard
  0 // return an integer exit code
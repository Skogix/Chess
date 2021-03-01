// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Scratch.Extra
type Move = {
  Piece: Piece
  From: Square
  To: Square
}
type MoveType =
  | Straight
  | Diagonal
type Direction = | Up | Down | Left | Right
let posToCollision (inputSquare:Square) (board:Board): Square list =
  let pos = inputSquare.Position
  let isEnemyPiece piece = piece.Color <> inputSquare.Piece.Value.Color
  let distance (dir:Direction) =
    match dir with
    | Up -> abs (pos.Row - 8)
    | Down -> pos.Row - 1
    | Left -> pos.Col - 1
    | Right -> abs (pos.Col - 8)
  let straightMoves =
    [
      [for i in [1 .. distance Up]    do {Col=pos.Col;Row=pos.Row+i}]
      [for i in [1 .. distance Down]  do {Col=pos.Col;Row=pos.Row-i}]
      [for i in [1 .. distance Left]  do {Col=pos.Col-i;Row=pos.Row}]
      [for i in [1 .. distance Right] do {Col=pos.Col+i;Row=pos.Row}]
    ] |> List.map board.GetSquares
  let diagonalMoves =
    [
     [for i in [1..(min (distance Up) (distance Right))]   do {Col=pos.Col+i;Row=pos.Row+i}]
     [for i in [1..(min (distance Up) (distance Left))]    do {Col=pos.Col-i;Row=pos.Row+i}]
     [for i in [1..(min (distance Down) (distance Right))] do {Col=pos.Col+i;Row=pos.Row-i}]
     [for i in [1..(min (distance Down) (distance Left))]  do {Col=pos.Col-i;Row=pos.Row-i}]
    ] |> List.map board.GetSquares
  let rec loop (rest:Square list) (out:Square list): Square list =
    match rest with
    | [] -> out
    | square::rest ->
      match square.Col, square.Row with
      | _ ->
        match square.Piece with
        | Some p ->
          match isEnemyPiece p with
          | true -> (square::out)
          | false -> out
        | None -> loop rest (square::out)
  let getMoves (move:MoveType): Square list =
    match move with
      | Straight -> straightMoves |> List.map (fun move -> loop move []) |> List.concat
      | Diagonal -> diagonalMoves |> List.map (fun move -> loop move []) |> List.concat
  getMoves Straight
//  getMoves Diagonal
[<EntryPoint>]
let main argv =
  let testBoard = Init.emptyBoard
  testBoard.AddPiece White Rook (2,3)
  let rookSquare = testBoard.Square (2,3)
  testBoard.AddPiece Black Pawn (2,5)
  testBoard.AddPiece Black Pawn (5,6)
  testBoard.AddPiece White Pawn (5,2)
//  testBoard.HighlightedSquares <- (oldPosToCollision rookSquare testBoard)
  testBoard.HighlightedSquares <- (posToCollision rookSquare testBoard)
  printBoard testBoard
  0 // return an integer exit code
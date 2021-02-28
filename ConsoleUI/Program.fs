open System
open System.Collections.Generic
open System.ComponentModel
open Chess


let getGlyph (piece:Piece option) =
  match piece with
  | None -> '.'
  | Some x ->
    match (x.Color, x.PieceType) with
    | (White, w) ->
      match w with
      | Pawn ->   '♙'
      | Bishop -> '♗'
      | Knight -> '♘'
      | Rook ->   '♖'
      | Queen ->  '♕'
      | King ->   '♔'
    | (Black, b) ->
      match b with
      | Pawn ->   '♟'
      | Bishop -> '♝'
      | Knight -> '♞'
      | Rook ->   '♜'
      | Queen ->  '♛'
      | King ->   '♚'
let printBoard (board:Board) =
  for square in board.Squares do
    Console.SetCursorPosition(square.Col, square.Row)
    printf "%c" (getGlyph square.Piece)
//    if square.Col % 8 = 0 then printfn ""
let printNotation (board:Board) =
  for square in board.Squares do
    printf "%s " square.Notation
    if square.Col % 8 = 0 then printfn ""
[<EntryPoint>]
let main argv =
  let board = Init.initBoard
  let emptyBoard = Init.emptyBoard
//  printBoard board
  printfn "%A" (board.Square 11)
//  printNotation board
  Console.ReadLine() |> ignore
  0 
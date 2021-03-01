module Scratch.Extra
open System
open Chess
let getGlyph (piece:Piece option) =
  match piece with
  | None -> '.'
  | Some x ->
    match (x.Color, x.PieceType) with
    | (Black, w) ->
      match w with
      | Pawn ->   '♙'
      | Bishop -> '♗'
      | Knight -> '♘'
      | Rook ->   '♖'
      | Queen ->  '♕'
      | King ->   '♔'
    | (White, b) ->
      match b with
      | Pawn ->   '♟'
      | Bishop -> '♝'
      | Knight -> '♞'
      | Rook ->   '♜'
      | Queen ->  '♛'
      | King ->   '♚'
let SquareIsHighlighted (square:Square) (highlighted:Square list): bool =
  highlighted
  |> List.contains square
let printBoard (board:Board) =
  Console.BackgroundColor <- ConsoleColor.DarkGray
  for square in board.Squares do
//    Console.SetCursorPosition(square.Col, square.Row)
    if (SquareIsHighlighted square board.HighlightedSquares) then Console.BackgroundColor <- ConsoleColor.Gray
    printf "%c" (getGlyph square.Piece); Console.BackgroundColor <- ConsoleColor.DarkGray
    if square.Col % 8 = 0 then printfn ""
let printNotation (board:Board) =
  for square in board.Squares do
    printf "%s " square.Notation
    if square.Col % 8 = 0 then printfn ""
let printPosition (board:Board) =
  for square in board.Squares do
    printf "%A " square.Position
    if square.Col % 8 = 0 then printfn ""

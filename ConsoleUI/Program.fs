open System
open System.Collections.Generic
open System.ComponentModel
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
let printBoard (board:Board) =
  for square in board.Squares do
//    Console.SetCursorPosition(square.Col, square.Row)
    printf "%c" (getGlyph square.Piece)
    if square.Col % 8 = 0 then printfn ""
let printNotation (board:Board) =
  for square in board.Squares do
    printf "%s " square.Notation
    if square.Col % 8 = 0 then printfn ""
let printPosition (board:Board) =
  for square in board.Squares do
    printf "%A " square.Position
    if square.Col % 8 = 0 then printfn ""
[<EntryPoint>]
let main argv =
  let board = Init.initBoard
  let emptyBoard = Init.emptyBoard
//  printPosition board
//  printfn "%A" (board.Square 11)
//  printBoard board
//  printfn "%A" (board.Square 11)
//  printBoard board
//  printfn "%A" (board.Square 31)
  
//  printBoard board
//  board.Move 21 31
//  printBoard board
//  printfn "%A" (Rules.GetMoves board 21)
  printfn "%A" (Rules.AllPosInRow 45)
  printfn "%A" (Rules.AllPosInCol 45)
  let huhu = (Rules.AllPosInCol 45) |> List.append (Rules.AllPosInRow 45) |> Rules.GetAllSquaresInList board 
  let newBoard = (Rules.AddSquaresToEmptyBoard huhu)
  printBoard newBoard

  Console.ReadLine() |> ignore
  0 
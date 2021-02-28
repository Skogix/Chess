open System
open System.Collections.Generic
open Chess
open Chess.Board

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
//let printBoard (board:Square list) f =
//  for s in board do
//    printf "%A " f
//    if s.Row = 8 then printfn ""
let printBoard board =
  for s in board do
    printf "%c" (getGlyph s.Piece)
    if s.Row = 8 then printfn ""
let printIndicies (board:Square list) =
  for s in board do
    printf "%i " s.Index
    if s.Row = 8 then printfn ""
let printNotations (board:Square list) =
  for s in board do
    printf "%s " s.Notation
    if s.Row = 8 then printfn ""
  
[<EntryPoint>]
let main argv =
  let board = Board.emptyBoard
  let agent = State.CommandAgent(board)
  agent.SendCommand (State.PostText "test")
//  printNotations board
//  printIndicies board
//  printBoard board
  Console.ReadLine() |> ignore
  0 
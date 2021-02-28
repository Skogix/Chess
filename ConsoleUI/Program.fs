open System
open System.Collections.Generic
open Chess
open Chess.BoardModule
open Chess.State

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
  for s in board.Squares do
    printf "%c" (getGlyph s.Piece)
    if s.Row = 8 then printfn ""
let printPositions (board:Board) =
  for s in board.Squares do
    printf "%i " s.Position
    if s.Row = 8 then printfn ""
let printNotation (board:Board) =
  for s in board.Squares do
    printf "%s " s.Notation
    if s.Row = 8 then printfn ""
[<EntryPoint>]
let main argv =
  let board = BoardModule.initBoard
  let agent = State.CommandAgent(board)
//  agent.SendCommand (State.PostText "test")
  let huhu = agent.PostAndReply Command.GetBoard
  printBoard huhu
  printNotation huhu
  printPositions huhu
  Console.ReadLine() |> ignore
  0 
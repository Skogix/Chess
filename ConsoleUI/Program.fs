open System
open System.Collections.Generic
open Chess
open Chess.BoardModule

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
[<EntryPoint>]
let main argv =
  let board = BoardModule.emptyBoard
//  let agent = State.CommandAgent(board)
//  agent.SendCommand (State.PostText "test")
  let square11 = board.Square 11
  printfn "%A" square11
//  printBoard agent.GetBoard
  
//  printNotations board
//  printIndicies board
//  printBoard board
  Console.ReadLine() |> ignore
  0 
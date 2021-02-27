// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Board
open State

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
let printState (state:State): unit =
  Console.Clear()
  for square in state.Board.Squares do
    let x,y = square.Position
    Console.SetCursorPosition(x,y)
    printf "%c" (getGlyph square.Piece)
    Console.SetCursorPosition(15, 1)
    printf "%A" state.History.Head
  Console.SetCursorPosition(1, 15)
let rec gameLoop (agent:State.Agent) =
  match Console.ReadKey(true).Key with
    | ConsoleKey.N -> agent.SendCommand (LogText "command N")
    | _ -> agent.SendCommand (LogText "hittade inte commandet")
  gameLoop agent
[<EntryPoint>]
let main argv =
      
  let agent = State.Agent()
  agent.State printState
  gameLoop agent
//  agent.State printState
//  agent.SendCommand (LogText "Uppdaterar state")
  
  0 // return an integer exit code
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
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
let printBoard (board:Board): unit =
  for square in board.Squares do
    let x,y = square.Position
    printf "%c" (getGlyph square.Piece)
    if y % 8 = 0 then printf "\n"
  printfn ""
//  for square in board.Squares do
//    let x,y = square.Position
//    Console.SetCursorPosition(x,y)
//    printf "%c" (getGlyph square.Piece)
//  Console.SetCursorPosition(1, 15)
//  Console.WriteLine()

[<EntryPoint>]
let main argv =
      
  let agent = Chess.State.Agent()
  let (skogix:Listener) = printBoard
  agent.RegisterListener skogix
  agent.SendCommand (LogText "Testar1")
  agent.SendCommand (LogText "Testar2")
//  agent.RegisterListener skogix
//  let huhu = agent.GetHistory
  
  Console.WriteLine()
  let board = Board()
//  printBoard board
  Console.ReadKey() |> ignore
  agent.SendCommand (LogText "Uppdaterar state")
  
    
  Console.ReadKey() |> ignore
  0 // return an integer exit code
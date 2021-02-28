namespace Chess
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
//open Chess
//open BoardModule
//open State
//
//let getGlyph (piece:Piece option) =
//  match piece with
//  | None -> '.'
//  | Some x ->
//    match (x.Color, x.PieceType) with
//    | (White, w) ->
//      match w with
//      | Pawn ->   '♙'
//      | Bishop -> '♗'
//      | Knight -> '♘'
//      | Rook ->   '♖'
//      | Queen ->  '♕'
//      | King ->   '♔'
//    | (Black, b) ->
//      match b with
//      | Pawn ->   '♟'
//      | Bishop -> '♝'
//      | Knight -> '♞'
//      | Rook ->   '♜'
//      | Queen ->  '♛'
//      | King ->   '♚'
//let printState (board:Board): unit =
//  Console.Clear()
//  for square in board.Squares do
//    let x,y = square.[x]
//    Console.SetCursorPosition(x,y)
//    printf "%c" (getGlyph square.Piece)
//  Console.SetCursorPosition(1, 15)
//let rec gameLoop (agent:State.Agent) =
//  match Console.ReadKey(true).Key with
//    | ConsoleKey.M -> agent.SendCommand (AddPiece (White, Rook))
//    | _ -> ()
//  gameLoop agent
//[<EntryPoint>]
//let main argv =
//  let board = Chess.ChessBoard.Instance
//  board.Action
//      
//  let agent = state.agent()
//  let huhu = state.apiagent
//  agent.State printState
//  agent.SendCommand (Command.AddPiece(White, Rook))
//  gameLoop (agent)
//  let agent = State.Agent()
//  agent.State printState
////  gameLoop agent
//  agent.SendCommand (AddPiece (White, Rook))
////  agent.State printState
//  agent.SendCommand (LogText "test")
//  agent.SendCommand (AddPiece (Black, Queen))
//  Console.Clear()
//  printfn "%A" agent
//  agent.SendCommand (LogText "Uppdaterar state")
  

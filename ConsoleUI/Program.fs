open System
open ChessCore
open ChessCore.Domain
open ChessCore.State
open ConsoleUI
open Ui
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  let agent = GameAgent(initFen)
//  let huhu = agent.GetBoard 
//  huhu |> printBoardByRank
  
  let b1 = agent.GetBoard
  let move = agent.Move {From=(1,1);To=(1,3)}
  move |> printBoard ByRank
  let moves = agent.SelectPiece (1,1)
  let a, b = moves
  printfn "%A" b
  moves |> printBoard ByRank
//  move |> printBoard ByRank 
//  move |> printBoard Positions
//  moves |> printfn "%A"

  
////  |> printBoardUi
//  |> printPositions
//  |> printBoardByRank
  
  
  
//  let game = Game.Create(initFen)
//  let availableMoves = game.SelectPiece 11
//  let moveReturn = game.Move availableMoves 1
  Console.ReadKey() |> ignore
  0
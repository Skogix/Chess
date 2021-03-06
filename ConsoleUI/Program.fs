open System
open System.Net
open ChessCore
open ChessCore.Domain
open ChessCore.State
open ConsoleUI
open Ui
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  Console.BackgroundColor <- ConsoleColor.Black
  let huhuFen = "rnbqkbnr/pppppppp/8/8/3B4/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
  let agent = GameAgent(huhuFen)
//  let huhu = agent.GetBoard 
//  huhu |> printBoardByRank
  let huhuBoard = agent.GetOutput
  let huhuSelectPiece = agent.SelectPiece (4,4)
  
  huhuBoard |> printBoard ByRank
  huhuSelectPiece |> printBoard ByRank
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
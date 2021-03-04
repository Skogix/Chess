open System
open ChessCore
open ChessCore.Board
open ConsoleUI
open Ui
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  
  let game = State.gameAgent(Utility.initBoardFen)
  let huhu = game.SelectSquare 42
  huhu |> printOutput
  Console.ReadKey() |> ignore
  0
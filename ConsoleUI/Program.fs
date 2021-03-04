open System
open ChessCore
open ChessCore.Board
open ConsoleUI
open Ui
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  
  let game = State.gameAgent(pawnFen)
  let huhu = game.SelectSquare 23
  let move = game.Move {From = 23; To = 24}
  huhu |> printOutput
//  move |> printOutput
  Console.ReadKey() |> ignore
  0
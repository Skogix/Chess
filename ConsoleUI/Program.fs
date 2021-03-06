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
  let agent = GameAgent(initFen)
//  let huhu = agent.GetBoard 
//  huhu |> printBoardByRank
  
  let b1 = agent.GetBoard
  let move = agent.Move {From=(1,1);To=(4,4)}
  let moves = { Board = move.Board
                SelectedPiece = Some (4,4)
                PossibleMoves = Some (Piece.getAllValidMoves (4,4) move.Board)}
  moves |> printBoard ByRank
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
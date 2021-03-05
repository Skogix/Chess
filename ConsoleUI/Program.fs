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
  
//  let move = agent.Move {From=(1,1);To=(1,3)}
//  move |> printBoardByRank
  let b1 = agent.GetBoard
  b1 |> printPositions
  let whiteRook: Content = Piece(Rook White)
  let b2 = b1.Add ((5,3),whiteRook)
  b2 |> printBoardByRank
  
//  let board = (Fen.createBoard initFen)
//  board
////  |> printBoardUi
//  |> printPositions
//  |> printBoardByRank
  
  
  
//  let game = Game.Create(initFen)
//  let availableMoves = game.SelectPiece 11
//  let moveReturn = game.Move availableMoves 1
  Console.ReadKey() |> ignore
  0
open System
open ChessCore
open ChessCore.Domain
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  
  let initFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
  
  
  
  let board = (Fen.createBoard initFen)
  board
  |> printfn "%A"
  
  
  
  
//  let game = Game.Create(initFen)
//  let availableMoves = game.SelectPiece 11
//  let moveReturn = game.Move availableMoves 1
  Console.ReadKey() |> ignore
  0
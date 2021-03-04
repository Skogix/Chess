open System
open ChessCore
open ChessCore.Board
open ConsoleUI
open Ui
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  let activeId = 11
  
  let bishop = "bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb w KQkq - 0 1"
  let rook = "rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr w KQkq - 0 1"
  let queen = "qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq w KQkq - 0 1"
  let king = "kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk w KQkq - 0 1"
  let pawn = "pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp w KQkq - 0 1"
  let knight = "nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn w KQkq - 0 1"
  let board = createBoard pawn
  let allFens = [bishop;rook;queen;king;pawn;knight]
  
  
  
  
  
  
  
  printBoard board ((Rules.getMoves activeId board))
  printIds board ((Rules.getMoves activeId board))
  printfn "pieceId: %A" activeId
  Rules.getMoves activeId board |> printfn "allMoves: %A"
  0
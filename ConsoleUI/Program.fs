open System
open ChessCore
open ChessCore.Board
let printBoard (squares:Square list) =
  let mutable counter = 0
  for x in squares do
    if counter % 8 = 0 then printfn ""
    counter <- counter + 1
    match x.Content with
      | Pawn White ->   '♙'
      | Bishop White -> '♗'
      | Knight White -> '♘'
      | Rook White ->   '♖'
      | Queen White ->  '♕'
      | King White ->   '♔'
      
      | Pawn Black ->   '♟'
      | Bishop Black -> '♝'
      | Knight Black -> '♞'
      | Rook Black ->   '♜'
      | Queen Black ->  '♛'
      | King Black ->   '♚'
      | None -> '.'
    |> printf "%c"
[<EntryPoint>]
let main argv =
  let fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
  let board = Board.createBoard fen
//  printfn "%A" board
//  Board.Notation 11 |> print
  printBoard board
  Console.ReadKey() |> ignore
  0
  
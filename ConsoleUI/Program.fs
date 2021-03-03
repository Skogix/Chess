open System
open ChessCore
open ChessCore.Board
open ChessCore.Domain
let printBoard (board:Board) (highlights:Id list) =
  let isHighlighted (id:Id) = highlights |> List.contains id
  let getGlyph = function
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
        | Empty -> '.'
  let printSquare (square:Square) =
    if (isHighlighted square.Id) then Console.ForegroundColor <- ConsoleColor.Cyan
    printf "%c" (getGlyph square.Content)
    Console.ForegroundColor <- ConsoleColor.White
    if Utility.getRankFromId square.Id = 8 then printfn ""
  printfn "%A to move" board.SideToMove
  printfn "Castles: %A" board.CastleRights
  printfn "EnPassant: %A" board.EnPassant
  printfn "FullMoves: %A" board.FullMove
  printfn "HalfMoves: %A" board.HalfMove
  board.Squares
  |> List.iter printSquare
    
let printIds (board:Board) =
  for x in board.Squares do
    printf "%i " x.Id
    if Utility.getRankFromId x.Id = 8 then printfn ""
   
let print x = printfn "%A" x
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  let move0 = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" |> createBoard 
  let move1 = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1" |> createBoard 
//  let move2 = "rnbqkbnr/pp1ppppp/8/2p5/4P3/8/PPPP1PPP/RNBQKBNR w KQkq c6 0 2" |> createBoard |> printBoard
//  let move3 = "rnbqkbnr/pp1ppppp/8/2p5/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2" |> createBoard |> printBoard
  let emptyBoard54 = "8/8/8/3R4/8/8/8/8 w KQkq - 0 1" |> createBoard
  let rookMoves = Rules.getMoves 54 emptyBoard54 |> List.concat
//  printBoard emptyBoard54 rookMoves
  printIds move0
//  printfn "%A" rookMoves
  printfn "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1"
  createFen move1 |> printfn "%s"
//  move0 |> printBoard
//  move0 |> printPositions
//  printBoard emptyBoardWithRook
//  printPositions emptyBoardWithRook
//  emptyBoardWithRookAt11 |> printBoard
//  emptyBoardWithRookAt11 |> printIds
//  Rules.getMoves 54 emptyBoardWithRookAt11 |> print
//  Rules.getMoves 54 emptyBoardWithRookAt11 |> List.map Utility.getNotation |> print
//  Utility.getNotation 54 |> print
  
  
  Console.ReadKey() |> ignore
  0
  
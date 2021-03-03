open System
open ChessCore
open ChessCore.Board
open ChessCore.Domain
open ChessCore.Pieces
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
  let emptyFen = "8/8/8/8/8/8/8/8 w KQkq - 0 1"
  let newBoardWithRook = createFen
  let emptyBoard = createBoard emptyFen
  let highlights = Rules.getMoves 44 emptyBoard
  printBoard emptyBoard highlights
  0
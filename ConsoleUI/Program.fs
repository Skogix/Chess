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
    
let printIds (board:Board) (highlights: Id list) =
  let isHighlighted (id:Id) = highlights |> List.contains id
  let printSquare (square:Square) =
    if (isHighlighted square.Id) then Console.ForegroundColor <- ConsoleColor.Cyan
    printf "%i" square.Id
    Console.ForegroundColor <- ConsoleColor.White
    if Utility.getRankFromId square.Id = 8 then printfn ""
  for x in board.Squares do
    printSquare x
    if Utility.getRankFromId x.Id = 8 then printfn ""
   
let print x = printfn "%A" x
[<EntryPoint>]
let main argv =
  Console.ForegroundColor <- ConsoleColor.White
  let activeId = 32
  
  let emptyFen = "bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb w KQkq - 0 1"
  let emptyBoard = createBoard emptyFen
  
  printBoard emptyBoard ((Rules.getMoves activeId emptyBoard))
//  let highlights = Rules.getMoves 45 emptyBoard
  printIds emptyBoard ((Rules.getMoves activeId emptyBoard))
  printfn "%A" (Rules.getMoves activeId emptyBoard)
  0
module ConsoleUI.Ui

open System
open ChessCore
open ChessCore.Board
open ChessCore.Domain
open ChessCore.State

let getGlyph (square:Content): char * ConsoleColor =
      match square with
      | Pawn White ->   '♙', ConsoleColor.DarkYellow
      | Bishop White -> '♗', ConsoleColor.DarkYellow
      | Knight White -> '♘', ConsoleColor.DarkYellow
      | Rook White ->   '♖', ConsoleColor.DarkYellow
      | Queen White ->  '♕', ConsoleColor.DarkYellow
      | King White ->   '♔', ConsoleColor.DarkYellow
      
      | Pawn Black ->   '♟', ConsoleColor.DarkCyan
      | Bishop Black -> '♝', ConsoleColor.DarkCyan
      | Knight Black -> '♞', ConsoleColor.DarkCyan
      | Rook Black ->   '♜', ConsoleColor.DarkCyan
      | Queen Black ->  '♛', ConsoleColor.DarkCyan
      | King Black ->   '♚', ConsoleColor.DarkCyan
      | Empty -> '.', ConsoleColor.Gray
let setHighlight (square:Square) (highlights:Highlights) (pieceColor:ConsoleColor) =
  let color =
    match highlights with
    | _ when highlights.ActivePiece = square.Id -> ConsoleColor.Blue
    | _ when highlights.Moves |> List.contains square.Id -> ConsoleColor.DarkRed
    | _ -> pieceColor
  Console.ForegroundColor <- color
let printOutput (output:Output) =
  let printSquare (highlights:Highlights) (square:Square)  =
    let glyph, pieceColor = getGlyph square.Content
    setHighlight square highlights pieceColor
    printf "%c" glyph
    if Utility.getFileFromId square.Id = 8 then printfn ""
    state
    |> List.iter (printSquare highlights)
let printIds (board:Board) (highlights: Id list) =
  let isHighlighted (id:Id) = highlights |> List.contains id
  let printSquare (square:Square) =
    if (isHighlighted square.Id) then Console.ForegroundColor <- ConsoleColor.Cyan
    printf "%i|" square.Id
//    Console.ForegroundColor <- ConsoleColor.White
    if Utility.getFileFromId square.Id = 8 then printfn ""
  for x in board.Squares do
    printSquare x
    if Utility.getFileFromId x.Id = 8 then printfn ""
let print x = printfn "%A" x




let bishopFen = "BBBBBBBB/BBBBBBBB/BBBBBBBB/BBBBBBBB/BBBBBBBB/BBBBBBBB/BBBBBBBB/BBBBBBBB w KQkq - 0 1"
let rookFen = "RRRRRRRR/RRRRRRRR/RRRRRRRR/RRRRRRRR/RRRRRRRR/RRRRRRRR/RRRRRRRR/RRRRRRRR w KQkq - 0 1"
let queenFen = "QQQQQQQQ/QQQQQQQQ/QQQQQQQQ/QQQQQQQQ/QQQQQQQQ/QQQQQQQQ/QQQQQQQQ/QQQQQQQQ w KQkq - 0 1"
let kingFen = "KKKKKKKK/KKKKKKKK/KKKKKKKK/KKKKKKKK/KKKKKKKK/KKKKKKKK/KKKKKKKK/KKKKKKKK w KQkq - 0 1"
let pawnFen = "PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP/PPPPPPPP w KQkq - 0 1"
let knightFen = "NNNNNNNN/NNNNNNNN/NNNNNNNN/NNNNNNNN/NNNNNNNN/NNNNNNNN/NNNNNNNN/NNNNNNNN w KQkq - 0 1"

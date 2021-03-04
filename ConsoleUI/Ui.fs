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
  let board, highlights = output
  let printSquare (square:Square) =
    let glyph, pieceColor = getGlyph square.Content
    setHighlight square highlights pieceColor
    printf "%c" glyph
    if Utility.getFileFromId square.Id = 8 then printfn ""
  board.Squares
  |> List.iter printSquare
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




let bishopFen = "bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb/bbbbbbbb w KQkq - 0 1"
let rookFen = "rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr/rrrrrrrr w KQkq - 0 1"
let queenFen = "qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq/qqqqqqqq w KQkq - 0 1"
let kingFen = "kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk/kkkkkkkk w KQkq - 0 1"
let pawnFen = "pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp/pppppppp w KQkq - 0 1"
let knightFen = "nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn/nnnnnnnn w KQkq - 0 1"

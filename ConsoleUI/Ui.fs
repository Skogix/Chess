module ConsoleUI.Ui

open System
open ChessCore.Domain
let getGlyph (piece:Piece) =
  match piece with
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
let getCharAndColor (content:Content) =
  match content with
  | Piece x -> getGlyph x
  | Empty -> '.', ConsoleColor.Gray
let printGlyph char color =
  Console.ForegroundColor <- color
  printf "%c" char
  Console.ForegroundColor <- ConsoleColor.White
let printBoardByRank (board:Board): Board =
  let listBoard = board |> Map.toList
  let printPiece ((_, file), content:Content) =
    let glyph, color = getCharAndColor content
    printGlyph glyph color
    if file = 8 then printfn ""
  listBoard
  |> List.iter printPiece
  board
let printBoardUi (board:Board): Board =
  let print (file, rank) content =
    Console.SetCursorPosition(rank, file) // åt fel håll pga file/rank är inte x/y
    let glyph, color = getCharAndColor content
    Console.ForegroundColor <- color
    printf "%c" glyph
    ()
  Console.Clear()
  board
  |> Map.iter print
  printfn ""
  board
let printPositions (board:Board): Board =
  let printPos ((rank, file), content) =
    printf "|%i%i" rank file
    if file = 8 then printfn "|"
  board
  |> Map.toList
  |> List.iter printPos
  board
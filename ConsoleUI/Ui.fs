module ConsoleUI.Ui

open System
open ChessCore.Domain
open ChessCore.State

type PrintType =
  | ByRank
  | ByCursorPosition
  | Positions
let whiteColor = ConsoleColor.DarkYellow
let blackColor = ConsoleColor.DarkBlue
let emptyColor = ConsoleColor.Gray
let highlightColor = ConsoleColor.Magenta
let selectedPieceColor = ConsoleColor.Cyan
let defaultBackgroundColor = ConsoleColor.Black
let defaultForegroundColor = ConsoleColor.White

let getGlyph (piece:Piece) =
  match piece with
    | Pawn White ->   '♙', whiteColor
    | Bishop White -> '♗', whiteColor
    | Knight White -> '♘', whiteColor
    | Rook White ->   '♖', whiteColor
    | Queen White ->  '♕', whiteColor
    | King White ->   '♔', whiteColor
    
    | Pawn Black ->   '♟', blackColor
    | Bishop Black -> '♝', blackColor
    | Knight Black -> '♞', blackColor
    | Rook Black ->   '♜', blackColor
    | Queen Black ->  '♛', blackColor
    | King Black ->   '♚', blackColor
let getCharAndColorFromContent (content:Content) =
  match content with
  | Piece x -> getGlyph x
  | Empty -> '.', emptyColor
let printGlyph char color =
  Console.ForegroundColor <- color
  printf "%c" char
  Console.ForegroundColor <- defaultForegroundColor
  Console.BackgroundColor <- defaultBackgroundColor 
let getRank (r:Rank) (board:Board) =
  board |> Map.toList |> List.filter (fun ((file, rank), content) -> rank = r)
  
  // todo Highlights
  
let printBoard (printType:PrintType) (output:Output)=
  let highlight x =
    match output.PossibleMoves with
    | Some highlightId -> if highlightId |> List.contains x then Console.BackgroundColor <- highlightColor
    | None -> ()
    match output.SelectedPiece with
    | Some id -> if id = x then Console.BackgroundColor <- selectedPieceColor
  let board = output.Board
  let ranks =
    [for r = 8 downto 1 do
      getRank r board] |> List.concat
  for x in ranks do
    let (file, rank), content = x
    let char, color = getCharAndColorFromContent content
    match printType with
    | ByRank ->
      highlight (file, rank)
      printGlyph char color
    | ByCursorPosition ->
      Console.SetCursorPosition(file, rank)
      printGlyph char color
    | Positions ->
      printf "%i%i " file rank
      ()
    if file = 8 then printfn ""
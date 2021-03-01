open System
open System.Collections.Generic
open System.ComponentModel
open Chess


let getGlyph (piece:Piece option) =
  match piece with
  | None -> '.'
  | Some x ->
    match (x.Color, x.PieceType) with
    | (Black, w) ->
      match w with
      | Pawn ->   '♙'
      | Bishop -> '♗'
      | Knight -> '♘'
      | Rook ->   '♖'
      | Queen ->  '♕'
      | King ->   '♔'
    | (White, b) ->
      match b with
      | Pawn ->   '♟'
      | Bishop -> '♝'
      | Knight -> '♞'
      | Rook ->   '♜'
      | Queen ->  '♛'
      | King ->   '♚'
let PosExistsInList (pos:Position) (highlighted:Position list): bool =
  highlighted
  |> List.contains pos
let printBoard (board:Board) =
  Console.BackgroundColor <- ConsoleColor.DarkGray
  for square in board.Squares do
//    Console.SetCursorPosition(square.Col, square.Row)
    if (PosExistsInList square.Position board.HighlightedSquares) then Console.BackgroundColor <- ConsoleColor.Gray
    printf "%c" (getGlyph square.Piece); Console.BackgroundColor <- ConsoleColor.DarkGray
    if square.Row % 8 = 0 then printfn ""
let printNotation (board:Board) =
  for square in board.Squares do
    printf "%s " square.Notation
    if square.Row % 8 = 0 then printfn ""
let printPosition (board:Board) =
  for square in board.Squares do
    printf "%A " square.Position
    if square.Row % 8 = 0 then printfn ""
[<EntryPoint>]
let main argv =
  let board = Init.initBoard
  let emptyBoard = Init.emptyBoard
//  printPosition board
//  printfn "%A" (board.Square 11)
//  printBoard board
//  printfn "%A" (board.Square 11)
//  printBoard board
//  printfn "%A" (board.Square 31)
  
//  printBoard board
//  board.Move 21 31
//  printBoard board
//  printfn "%A" (Rules.GetMoves board 21)
//  printfn "%A" (Rules.AllPosInRow 45)
//  printfn "%A" (Rules.AllPosInCol 45)
//  let boardWithOnlyAddedSquares = (Rules.AllPosInCol 45) |> List.append (Rules.AllPosInRow 45) |> Rules.GetAllSquaresInList board |> Rules.AddSquaresToEmptyBoard
//  let highlightedList = (Rules.AllPosInCol 45) |> List.append (Rules.AllPosInRow 45)
//  boardWithOnlyAddedSquares.HighlightedSquares <- highlightedList
//  let newBoard = (Rules.AddSquaresToEmptyBoard huhu)
//  printBoard newBoard
//  printfn "%A" newBoard.HighlightedSquares
//  printBoard boardWithOnlyAddedSquares
//  let checkPos = createPos (4,2)
//  (emptyBoard.Square (4,2)).Piece <- Some { Color = White
//                                            PieceType = Rook }
//  (emptyBoard.Square (4,6)).Piece <- Some { Color = Black
//                                            PieceType = Pawn }
//  let positionsToHighLight = Rules.GetValidMoves checkPos emptyBoard
//  emptyBoard.HighlightedSquares <- positionsToHighLight
//  printBoard emptyBoard
//  printfn "%A" positionsToHighLight
//  printfn "%A" allRow
//  let allMoves = (Rules.GetMoves 11 emptyBoard)
//  let los = (Rules.LineOfSight 11 allMoves)
//  emptyBoard.HighlightedSquares <- los
  
//  printBoard emptyBoard
  
//  printfn "%A" (List.rev [1..10])

  Console.ReadLine() |> ignore
  0 
module ChessCore.Board

open System
open ChessCore.Domain
type Board = {
  Squares: Square list 
  SideToMove: Color
  CastleRights: CastleRights list
  EnPassant: Position option
  HalfMove: MoveCounter
  FullMove: MoveCounter
} with
  member this.Square id = this.Squares |> List.find (fun x -> x.Id = id)
let getPieceFromChar (char:char) color =
  match Char.ToLower char  with
  | 'p' -> Pawn color
  | 'r' -> Rook color
  | 'n' -> Knight color
  | 'b' -> Bishop color
  | 'q' -> Queen color
  | 'k' -> King color
  | 'e' -> EnPassant color
  | _ -> failwith "getPieceFromChar"
let createBoard (fen:string) =
//  let move1 = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPPPPPP/RNBQKBNR b KQkq e3 0 1"
  let not = fen.Split ' '
  let (boardNotation, sideToMove, castleRights, enPassant, halfMove, fullMove) = (not.[0],not.[1], not.[2], not.[3], not.[4], not.[5])
  let convertNotation =
    let rec loop rest out =
      match rest with
      | [] -> out
      | x::rest ->
        match x with
        | '1'|'2'|'3'|'4'|'5'|'6'|'7'|'8' -> loop rest ([for i in [1..(x |> Char.GetNumericValue |> int)] do Empty]::out)
        | 'p'|'r'|'n'|'b'|'q'|'k' -> loop rest ([(getPieceFromChar x Black)]::out)
        | 'P'|'R'|'N'|'B'|'Q'|'K' -> loop rest ([(getPieceFromChar x White)]::out)
        | _ -> loop rest (out)
    loop (boardNotation |> Seq.toList) [] |> List.rev
  let createSquares (id, content) = {Id = id;Content = content}
  let outSquares =
    convertNotation |> List.concat
    |> List.zip Utility.squareIds
    |> List.map createSquares
  let outSideToMove =
    match sideToMove with
    | "w" -> White
    | "b" -> Black
  let outCastleRights =
    [for char in castleRights do
      match char with
      | 'Q' -> WhiteQueenSide
      | 'K' -> WhiteKingSide
      | 'q' -> BlackQueenSide
      | 'k' -> BlackKingSide]
  let outEnPassant: Position option =
    match enPassant with
    | "-" -> None
    | x -> Some (Utility.getPosFromNotation x)
  {
    Squares = outSquares
    SideToMove = outSideToMove
    CastleRights = outCastleRights
    EnPassant = outEnPassant
    HalfMove = halfMove |> int
    FullMove = fullMove |> int }
let createFen (board:Board) =
  let getNotationFromPiece content color =
    match content with
    | Pawn _ -> 'p'
    | Rook _ -> 'r'
    | Knight _ -> 'n'
    | Bishop _ -> 'b'
    | Queen _ -> 'q'
    | King _ -> 'k'
    |> (fun x -> if color = White then Char.ToUpper x else x)
  let createFen (squares:Square list) =
    [
      let rec loop (rest:Square list) (out:Char list) counter =
        match rest with
        | x::rest when x.Content <> Empty && counter = 0 ->
          match x.Content with
          | Pawn color | Rook color | Knight color | Bishop color | Queen color | King color -> loop rest ((getNotationFromPiece x.Content color)::out) counter
        | x::rest when x.Content = Empty -> loop rest out (counter+1)
        | x::rest when x.Content <> Empty && counter > 0 -> loop (x::rest) ((counter |> string |> char)::out) 0
        | [] when counter > 0 -> ((counter |> string |> char)::out)
        | _ -> out
      loop squares [] 0
    ] @ [['/']]
  let pieces =
    [
      for rank = 8 downto 1 do
        [for file = 1 to 8 do
           board.Square (rank*10+file)] |> List.rev
    ]
    |> List.map createFen
    |> List.concat
    |> List.concat
    |> String.Concat
    |> (fun x -> x.Remove(x.Length - 1, 1))
  let sideToMove = match board.SideToMove with
    | White -> "w"
    | Black -> "b"
  let castleRights =
    [for right in board.CastleRights do
      match right with
      | WhiteKingSide -> "K"
      | WhiteQueenSide -> "Q"
      | BlackKingSide -> "k"
      | BlackQueenSide -> "q"] |> List.fold (+) ""
  let enPassant = match board.EnPassant with
    | Some (file, rank) -> (Utility.getNotationFromPosition file rank )
    | None -> "-"
  pieces + " " +
  sideToMove + " " +
  castleRights + " " +
  enPassant + " " +
  (board.HalfMove |> string) + " " +
  (board.FullMove |> string)
  
//  {
//    Squares = outSquares
//    SideToMove = outSideToMove
//    CastleRights = outCastleRights
//    EnPassant = outEnPassant
//    HalfMove = halfMove |> int
//    FullMove = fullMove |> int }

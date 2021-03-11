module ChessCore.Fen

open ChessCore.Domain

let createBoard (fen:Fen): Board =
  let pieces = (fen.Split ' ').[0].ToCharArray()
  let convertNumberToDot char =
    match char with
    | '1'|'2'|'3'|'4'|'5'|'6'|'7'|'8' ->
      let toInt = char |> System.Char.GetNumericValue |> int
      [for _ = 1 to toInt do ['.']]
    | _ -> [[char]]
  let charToPiece (char:char): Content =
    let color =
      match System.Char.IsUpper char with
      | true -> White
      | false -> Black
    let piece: Content =
      match System.Char.ToLower char with
      | 'p' -> Piece (Pawn color)
      | 'r' -> Piece (Rook color)
      | 'n' -> Piece (Knight color)
      | 'b' -> Piece (Bishop color)
      | 'q' -> Piece (Queen color)
      | 'k' -> Piece (King color)
      | '.' -> Empty
      | _ -> failwith "createBoard.charToPiece"
    piece
  pieces
  |> Array.filter (fun char -> char <> '/')
  |> Array.toList
  |> List.map convertNumberToDot
  |> List.concat
  |> List.concat
  |> List.map charToPiece
  |> List.zip boardIds
  |> Map.ofList
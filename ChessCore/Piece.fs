module ChessCore.Piece

open ChessCore.Domain
let getAllValidMoves (id:Id) (board:Board): Id list =
  let piece = board |> Map.find id
  let (file, rank) = id
  let removeSelected input = input <> id
  let removeOutsideBoard input =
    let file, rank = input
    rank >= 1 &&
    rank <= 8 &&
    file >= 1 &&
    file <= 8
  let convertToId (file, rank): Id = (file, rank)
  let straightMoves: Id list list=
    [
      [for x = 8 downto file do (x, rank)] 
      [for x = 1 to file do (x, rank)]
      [for x = 8 downto rank do (file, x)]
      [for x = 1 to rank do (file, x)]
    ]
  let diagonalMoves =
    [
      [for x = 1 to (max file rank) do (file+x, rank+x)]
      [for x = 1 to (max file rank) do (file+x, rank-x)]
      [for x = (max file rank) downto 1 do (file-x, rank-x)]
      [for x = (max file rank) downto 1 do (file-x, rank+x)]
    ]
  let knightMoves =
    [
      [(file+2, rank+1);(file+2,rank-1)]
      [(file+1, rank+2);(file-1,rank+2)]
      [(file-2, rank+1);(file-2,rank-1)]
      [(file+1, rank-2);(file-1,rank-2)]
    ]
  let pawnMoves =
    [
      match id, piece with
      | (file, rank), Piece(Pawn White) when rank = 2 -> [(file,rank+1);(file,rank+2)]
      | (file, rank), Piece(Pawn Black) when rank = 7 -> [(file,rank-1);(file,rank-2)]
      | (_, rank), Piece(Pawn White) -> [file,rank+1]        
      | (_, rank), Piece(Pawn Black) -> [file,rank-1]
      | _ -> []
    ]
  
  let allMoves =
    match piece with
    | Piece (Rook _) -> straightMoves 
    | Piece (Bishop _) -> diagonalMoves 
    | Piece (Queen _) -> (straightMoves @ diagonalMoves) 
    | Piece (Knight _) -> knightMoves 
    | Piece (Pawn _) -> pawnMoves 
    | _ -> []
  let checkBlockedPath (path:Id list) =
    path
    |> List.filter removeSelected
    |> List.filter removeOutsideBoard
    |> List.rev
    |> print
  let removeAllBlockedPaths =
    allMoves
    |> List.map checkBlockedPath
  let output = 
    allMoves
    |> List.rev
    |> List.concat
    |> List.map convertToId
    |> List.filter removeSelected
    |> List.filter removeOutsideBoard
  output
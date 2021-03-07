module ChessCore.Piece

open ChessCore.Domain

let getAllValidMoves (id:Id) (board:Board): Id list =
  let piece =
    printfn "map.find %A" (board |> Map.find id)
    printfn "Id: %A" id
    board |> Map.find id
  let (file, rank) = id
  let straightMoves =
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
  let convertToId (file, rank): Id = (file, rank)
  let removeSelected input = input <> id
  let removeOutsideBoard input =
    let file, rank = input
    rank >= 1 &&
    rank <= 8 &&
    file >= 1 &&
    file <= 8
  match piece with
  | Piece (Rook _) ->
    straightMoves |> List.concat
  | Piece (Bishop _) ->
    diagonalMoves |> List.concat
  | Piece (Queen _) ->
    (straightMoves @ diagonalMoves) |> List.concat
  | _ -> []
  |> List.map convertToId
  |> List.filter removeSelected
  |> List.filter removeOutsideBoard

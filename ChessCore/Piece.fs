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
      [for x = file     to 8 do (x, rank)]
      [for x = 8 downto rank do (file, x)]
      [for x = rank     to 8 do (file, x)]
    ]
  match piece with
  | Piece (Rook _) ->
    straightMoves |> List.concat
  | _ -> []
module ChessCore.Piece

open ChessCore.Domain

let getAllValidMoves (id:Id) (board:Board): Move list =
  let piece =
    printfn "map.find %A" (board |> Map.find id)
    printfn "Id: %A" id
    board |> Map.find id
  let (file, rank) = id
  let move (file:File) (rank:Rank) = {From=id;To=(file, rank)}
  let straightMoves =
    [
      [for x = 8 downto file do [move x rank]]
      [for x = file     to 8 do [move x rank]]
      [for x = 8 downto rank do [move file x]]
      [for x = rank     to 8 do [move file x]]
    ]
  match piece with
  | Piece (Rook _) ->
    straightMoves |> List.concat |> printfn "%A"
    straightMoves |> List.concat |> List.concat
  | _ -> []
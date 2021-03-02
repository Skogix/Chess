module ChessCore.Utility
let squareIds =
  [
    [81..88] @
    [71..78] @
    [61..68] @
    [51..58] @
    [41..48] @
    [31..38] @
    [21..28] @
    [11..18]
  ] |> List.concat
let notation id =
  let file =
    match (id/1) % 10 with
    | 1 -> 8
    | 2 -> 7
    | 3 -> 6
    | 4 -> 5
    | 5 -> 4
    | 6 -> 3
    | 7 -> 2
    | 8 -> 1
    | _ -> failwith "notation file"
  let rank =
    match ((id/10)%10) with
    | 1 -> "a"
    | 2 -> "b"
    | 3 -> "c"
    | 4 -> "d"
    | 5 -> "e"
    | 6 -> "f"
    | 7 -> "g"
    | 8 -> "h"
    | _ -> failwith "notation rank"
  (rank.ToString() + file.ToString())
notation 11
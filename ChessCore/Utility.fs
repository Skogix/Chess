module ChessCore.Utility

let notation col row =
  let col =
    match col with
    | 1 -> 'A'
    | 2 -> 'B'
    | 3 -> 'C'
    | 4 -> 'D'
    | 5 -> 'E'
    | 6 -> 'F'
    | 7 -> 'G'
    | 8 -> 'H'
    | _ -> ' '
  let row =
    match row with
    | 1 -> '1'
    | 2 -> '2'
    | 3 -> '3'
    | 4 -> '4'
    | 5 -> '5'
    | 6 -> '6'
    | 7 -> '7'
    | 8 -> '8'
    | _ -> ' '
  (col.ToString() + row.ToString())

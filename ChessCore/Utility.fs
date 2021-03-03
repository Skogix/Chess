module ChessCore.Utility

open ChessCore.Domain

let squareIds =
  [
    [18..10..88] @
    [17..10..87] @
    [16..10..86] @
    [15..10..85] @
    [14..10..84] @
    [13..10..83] @
    [12..10..82] @
    [11..10..81]
  ] |> List.concat
let getNotationFromId id =
  let file =
    match id/10 with
    | 1 -> 1
    | 2 -> 2
    | 3 -> 3
    | 4 -> 4
    | 5 -> 5
    | 6 -> 6
    | 7 -> 7
    | 8 -> 8
    | _ -> failwith "notation file"
  let rank =
    match id%10 with
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
let getPosFromNotation (not:string): Position =
    let file =
      match not.[0] with
      | 'a' -> 1
      | 'b' -> 2
      | 'c' -> 3
      | 'd' -> 4
      | 'e' -> 5
      | 'f' -> 6
      | 'g' -> 7
      | 'h' -> 8
      | _ -> failwith "getIdFromNotation"
    let rank =
      match not.[1] with
        | '1' -> 1
        | '2' -> 2
        | '3' -> 3
        | '4' -> 4
        | '5' -> 5
        | '6' -> 6
        | '7' -> 7
        | '8' -> 8
        | _ -> failwith "getIdFromNotation"
    (file , rank)
let getIdFromNotation not: Id =
  let file, rank = getPosFromNotation not
  (file*10+rank)
let getFileFromId (id:Id): File = id/10
let getRankFromId (id:Id): Rank = id%10
let getNotationFromPosition (file:File) (rank:Rank): Notation = getNotationFromId (rank*10+file)

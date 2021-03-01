module Scratch.Backup


//  let rec loop (rest:Square list) (out:Square list): Square list =
//    match rest with
//    | [] -> out
//    | square::rest ->
//      match square.Col, square.Row with
//      | _ ->
//        match square.Piece with
//        | Some p ->
//          match isEnemyPiece p with
//          | true -> (square::out)
//          | false -> out
//        | None -> loop rest (square::out)
//  let getMoves (move:MoveType): Square list =
//    let distanceToEdge (dir:Direction) =
//      match dir with
//      | Up -> abs (inputPos.Row - 8)
//      | Down -> inputPos.Row - 1
//      | Left -> inputPos.Col - 1
//      | Right -> abs (inputPos.Col - 8)
//    let kingMoves =
//      let checkPos col row = col <= 8 && col >= 1 && row <= 8 && row >= 1
//      let posList =
//        let row = inputPos.Row
//        let col = inputPos.Col
//        [
//          (col+1, row);(col-1, row);(col, row+1);(col, row-1)
//        ]
//      [ for col, row in posList do
//          if (checkPos col row) then [createPos(col, row)] 
//      ] |> List.map board.GetSquares
//    let horseMoves =
//      let checkPos col row = col <= 8 && col >= 1 && row <= 8 && row >= 1
//      let posList =
//        let row = inputPos.Row
//        let col = inputPos.Col
//        [
//          (col-1,row+2);(col+1,row+2) // up
//          (col-1,row-2);(col+1,row-2) // down
//          (col-2,row-1);(col-2,row+1) // left
//          (col+2,row-1);(col+2,row+1) // left
//        ]
//      [ for col, row in posList do
//          if (checkPos col row) then [createPos(col, row)] 
//      ] |> List.map board.GetSquares
//    let pawnMoves =
//      let checkPos col row = col <= 8 && col >= 1 && row <= 8 && row >= 1
//      let posList =
//        let row = inputPos.Row
//        let col = inputPos.Col
//        [
//          match inputSquare.Piece.Value.Color, inputSquare.Position.Row with
//          | White, 2 -> (col, row+1);(col, row+2)
//          | Black, 7 -> (col, row-1);(col, row-2)
//          | White, _ -> (col, row+1)
//          | Black, _ -> (col, row-1)
//          | _, _ -> ()
//        ]
//      [ for col, row in posList do
//          if (checkPos col row) then [createPos(col, row)] 
//      ] |> List.map board.GetSquares
//    let straightMoves =
//      let posList =
//        let row = inputPos.Row
//        let col = inputPos.Col
//        [
//          for i in [1..distanceToEdge Up] do (col, row+i)
//          for i in [1..distanceToEdge Up] do (col, row-i)
//          for i in [1..distanceToEdge Up] do (col+i, row)
//          for i in [1..distanceToEdge Up] do (col-i, row)
//          
////          (col+1, row);(col-1, row);(col, row+1);(col, row-1)
//        ]
//      [ for col, row in posList do
//          if (posInsideBoard col row) then [createPos(col, row)] 
//      ] |> List.map board.GetSquares
////    let straightMoves =
////      [
////        [for i in [1 .. distanceToEdge Up]    do {Col=inputPos.Col;Row=inputPos.Row+i}]
////        [for i in [1 .. distanceToEdge Down]  do {Col=inputPos.Col;Row=inputPos.Row-i}]
////        [for i in [1 .. distanceToEdge Left]  do {Col=inputPos.Col-i;Row=inputPos.Row}]
////        [for i in [1 .. distanceToEdge Right] do {Col=inputPos.Col+i;Row=inputPos.Row}]
////      ] |> List.map board.GetSquares
//    let diagonalMoves =
//      [
//       [for i in [1..(min (distanceToEdge Up) (distanceToEdge Right))]   do {Col=inputPos.Col+i;Row=inputPos.Row+i}]
//       [for i in [1..(min (distanceToEdge Up) (distanceToEdge Left))]    do {Col=inputPos.Col-i;Row=inputPos.Row+i}]
//       [for i in [1..(min (distanceToEdge Down) (distanceToEdge Right))] do {Col=inputPos.Col+i;Row=inputPos.Row-i}]
//       [for i in [1..(min (distanceToEdge Down) (distanceToEdge Left))]  do {Col=inputPos.Col-i;Row=inputPos.Row-i}]
//      ] |> List.map board.GetSquares

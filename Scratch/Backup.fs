module Scratch.Backup

open Chess

let oldPosToCollision (square:Square) (board:Board) =
  let pos = square.Position
  let isEnemyPiece (piece:Piece) = piece.Color <> square.Piece.Value.Color
  let up =    [for row in [pos.Row%10+1 .. 8] do createPos(pos.Col, row)]
  let right = [ for col in [pos.Col%10+1 .. 8] do createPos(col, pos.Row)]
  let upright = [
    let smallest =
      min up.Length right.Length
    for x in [1..smallest] do
        createPos(pos.Col+x, pos.Row+x)
  ]
  let down =  [1 .. pos.Row%10-1]
  let left =  [1 .. pos.Col%10-1]
  let getPosUp = [
    for p in up do
      board.Square(p.Col, p.Row)
  ]
  let getPosRight = [
    for p in right do
      board.Square(p.Col, p.Row)
  ]
  let getPosUpRight = [
    for pos in upright do board.Square(pos.Col, pos.Row)
  ]
  let getPosDown = [ for row in down do board.Square(square.Col, row) ]
  let getPosLeft = [ for col in left do board.Square(col, square.Row) ]
  let rec loop (rest:Square list) (out:Square list)  =
    match rest with
    | [] -> out
    | x::rest ->
      match x.Col, x.Row with
      | _, (1 | 8) -> (x::out)
      | (1 | 8), _ -> (x::out)
      | _ -> 
        match x.Piece with
        | Some piece ->
          match isEnemyPiece piece with
          | true -> (x::out)
          | false -> out
        | None -> loop rest (x::out)
  printfn "%A" upright
  loop getPosUpRight []

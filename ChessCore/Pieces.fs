module ChessCore.Pieces

open ChessCore.Board
open ChessCore.Domain
let checkForCollision listlist =
  listlist
type Direction =
  | Up
  | Down
  | Left
  | Right
let getBasicMoves (square:Square) (board:Board) =
  let file = Utility.getFileFromId square.Id
  let rank = Utility.getRankFromId square.Id
  let id = square.Id
  let toEdgeUp = abs(rank - 8)
  let toEdgeDown = abs(rank - 1)
  let toEdgeLeft = abs(file - 1)
  let toEdgeRight = abs(file - 8)
  let diagonalMoves: int list list =
    [
      [for x = 1 to min toEdgeUp toEdgeRight do (((file+x)*10)+(rank+x)) ] 
      [for x = 1 to min toEdgeDown toEdgeRight do (((file+x)*10)+(rank-x)) ]
      [for x = 1 to min toEdgeDown toEdgeRight do (((file-x)*10)+(rank-x)) ]
      [for x = 1 to min toEdgeUp toEdgeLeft do (((file-x)*10)+(rank+x)) ]
    ]
  let straightMoves: int list list =
    [
      [for x = rank+1     to 8 do (file*10+x)]
      [for x = rank-1 downto 1 do (file*10+x)] 
      [for x = file+1     to 8 do (x*10+rank)]
      [for x = file-1 downto 1 do (x*10+rank)]
    ]
  let pawnMoves: int list list =
    [
      match square.Id, square.Content with
      | id, Pawn White when id%10 = 2 -> [(id+1);(id+2)]
      | id, Pawn Black when id%10 = 7 -> [(id-1);(id-2)]
      | id, Pawn White -> [id+1]
      | id, Pawn Black -> [id-1]
      | _ -> []
    ]
  let knightMoves: int list list =
    [
      [
        id+10+2;id+10-2
        id+20+1;id+20-1
        id-20+1;id-20-1
        id-10+2;id-10-2
      ]
    ]
  match square.Content with
  | Rook _ -> straightMoves
  | Bishop _ -> diagonalMoves
  | Queen _ -> diagonalMoves @ straightMoves
  | King _ -> (diagonalMoves @ straightMoves) |> List.map (fun x -> [x.Head])
  | Pawn _ -> pawnMoves
  | Knight _ -> knightMoves
  | _ -> []
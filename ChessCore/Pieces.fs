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
let getStraightMoves (square:Square) (board:Board) =
  let getAllMoves (id:Id):Id list list =
    let file = Utility.getFileFromId id
    let rank = Utility.getRankFromId id
    [
      [for x = rank+1     to 8 do (file*10+x)]
      [for x = rank-1 downto 1 do (file*10+x)] 
      [for x = file+1     to 8 do (x*10+rank)]
      [for x = file-1 downto 1 do (x*10+rank)]
    ]
  square.Id
  |> getAllMoves
  |> checkForCollision
let getDiagonalMoves (square:Square) (board:Board): Id list list =
  let getAllMoves id =
    let file = Utility.getFileFromId id
    let rank = Utility.getRankFromId id
    printfn "id %A%A" file rank
    [
      //  45
      //                                        x*10+x = 88 
      [for x = max (file+0) (rank+0) to 8 do ( x * 10 + x + 1)] |> List.rev // up right
//      [for x = max (file+0) (rank+1) to 8 do (x*10-x) ]
    ]
  square.Id
  |> getAllMoves
  |> checkForCollision
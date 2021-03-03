module ChessCore.Pieces

open ChessCore.Board
open ChessCore.Domain
let checkForCollision listlist =
  listlist
module Rook =
  let getMoves (square:Square) (board:Board) =
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
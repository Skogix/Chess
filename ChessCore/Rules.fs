module ChessCore.Rules

open ChessCore.Board
open ChessCore.Domain
let getMoves (id:Id) (board:Board) =
  let square = board.Square id
  match square.Content with
  | Rook _ -> Pieces.getStraightMoves square board
  | Bishop _ -> Pieces.getDiagonalMoves square board
  | _ -> []
  |> List.concat
module ChessCore.Rules

open ChessCore.Board
open ChessCore.Domain
let getMoves (id:Id) (board:Board) =
  let square = board.Square id
  match square.Content with
  | Rook _ -> Pieces.Rook.getMoves square board
  | _ -> []
  |> List.concat
module ChessCore.Rules

open ChessCore.Board
open ChessCore.Domain
let getMoves (id:Id) (board:Board) =
  let square = board.Square id
  match square.Content with
  | Rook _ -> Pieces.getBasicMoves square board
  | Bishop _ -> Pieces.getBasicMoves square board
  | Queen _ -> Pieces.getBasicMoves square board
  | King _ -> Pieces.getBasicMoves square board
  | Pawn _ -> Pieces.getBasicMoves square board
  | Knight _ -> Pieces.getBasicMoves square board
  | _ -> []
  |> List.concat
module ChessCore.Domain

type Rank = int
type File = int
type Id = File * Rank
type Color = White | Black
type Fen = string
type Piece =
  | Pawn of Color
  | Bishop of Color
  | Knight of Color
  | Rook of Color
  | Queen of Color
  | King of Color
type Content =
  | Empty
  | Piece of Piece
type Square = Id * Content
type Board = Id -> Content list
type State = {
  Fen: string
  Board: Board
  // all annan info som behovs eller bara fen?
}

// IO
type Move =
  | From of Id
  | To of Id
type Command =
  | Move of Move
  | SelectPiece of Id
type Output = {
  Board: Board
  Commands: Command list
  // eventuell extra info som highlighted squares osv
}

// State
type CreateBoard = Fen -> Board
type CreateGame = Fen -> MailboxProcessor<Command>

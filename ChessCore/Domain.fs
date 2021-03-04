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
type Board = Map<Id, Content>
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

















let initFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
let boardIds: Id list =
  [
    for x = 8 downto 1 do
      for y = 1 to 8 do
        (x,y)
  ]
let emptyBoard: Board =
  boardIds
  |> List.map (fun id -> (id, Empty))
  |> Map.ofList

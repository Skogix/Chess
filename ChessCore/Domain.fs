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




















let initFen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
let boardIds: Id list =
  [
    for x = 8 downto 1 do
      for y = 1 to 8 do
        (y,x)
  ]
let emptyBoard: Board =
  boardIds
  |> List.map (fun id -> (id, Empty))
  |> Map.ofList

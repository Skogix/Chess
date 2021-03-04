module ChessCore.State

open ChessCore.Board
open ChessCore.Domain
type Highlights = {
  ActivePiece: Id
  Moves: Id list
}
type Output =
  | ShowMoves of Squares * Highlights
  | BoardState of Squares
type Move = {
  From: Id
  To: Id
}
type Command =
  | SelectSquare of AsyncReplyChannel<Output> * Id
  | Move of AsyncReplyChannel<Output> * Move
type State = Board
type gameAgent (init) =
  let handleCommand (command:Command) (state:State):State =
    match command with
    | SelectSquare (rc, squareId) ->
      let moves: Id list = Pieces.getBasicMoves (state.Square squareId) (state) |> List.concat
      let highlights = {ActivePiece = squareId; Moves = moves}
      rc.Reply (ShowMoves (state.Squares, highlights))
      state
    | Move (rc, move) ->
      printfn "MOVE %A" move
      rc.Reply (BoardState state.Squares)
      state
    | _ -> failwith "handleCommand"
      
  let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
    let rec loop (state:State) = async {
      let! command = inbox.Receive()
      let newState = handleCommand command state
      return! loop newState
    }
    loop (init |> createBoard)
    )
  member this.SelectSquare (id:Id) = mailbox.PostAndReply (fun channel -> SelectSquare(channel, id))
  member this.Move (move:Move) = mailbox.PostAndReply (fun channel -> Move(channel, move))
  
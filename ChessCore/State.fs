module ChessCore.State

open ChessCore.Board
open ChessCore.Domain
type Highlights = {
  ActivePiece: Id
  Moves: Id list
}
type Output = Board * Highlights
type Command =
  | SelectSquare of AsyncReplyChannel<Output> * Id
type State = Board
type gameAgent (init) =
  let handleCommand (command:Command) (state:State):State =
    match command with
    | SelectSquare (rc, squareId) ->
      let moves: Id list = Pieces.getBasicMoves (state.Square squareId) (state) |> List.concat
      rc.Reply (state, {ActivePiece = squareId;Moves = moves})
      state
      
  let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
    let rec loop (state:State) = async {
      let! command = inbox.Receive()
      let newState = handleCommand command state
      return! loop newState
    }
    loop (init |> createBoard)
    )
  member this.PostAndReply command = mailbox.PostAndReply (fun channel -> command(channel))
  member this.SelectSquare id = mailbox.PostAndReply (fun channel -> SelectSquare(channel, id))
  
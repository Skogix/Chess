module Scratch.state

open ChessCore
open ChessCore.Domain

module State =
  type Command =
    | GetBoard of AsyncReplyChannel<Board>
    | SendMessage of string
    | AddPiece of AsyncReplyChannel<Board> * Piece
  type State = {
    Board: Board
    History: Command list
  }
  type Agent(board:Board) =
    let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
      let rec loop (state:State) = async {
        let! command = inbox.Receive()
        let board = state.Board
        match command with
        | SendMessage msg ->
          let newHistory = {state with History = command::state.History}
          return! loop state
        | GetBoard rc ->
          rc.Reply state.Board
          return! loop state
        | AddPiece (rc, piece) ->
          0
        return! loop state
      }
      loop {State.Board = board; State.History = []})
    member this.SendMessage msg = mailbox.Post (Command.SendMessage(msg))
    member this.GetBoard = mailbox.PostAndReply (fun channel -> GetBoard(channel))

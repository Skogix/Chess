module ChessCore.State

open System.Runtime.CompilerServices
open ChessCore.Domain


type StateAgent<'T>(init:'T) =
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop stateHistory = async {
      let! command = inbox.Receive()
      match command with
      | Add (rc, newState) ->
        rc.Reply newState
        loop (newState::stateHistory)
      | Get rc -> rc.Reply stateHistory.Head
      return! loop stateHistory
    }
    loop [init]
    )
  member this.Add (newState:'T) = mailbox.PostAndReply (fun channel -> Add(channel, newState))
  member this.Get = mailbox.PostAndReply (fun channel -> Get(channel))
type GameAgent(initFen) =
  let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
    let boardState = StateAgent<Board>(Fen.createBoard initFen)
    let rec loop commandHistory = async {
      let! command = inbox.Receive()
      match command with
      | GetBoard rc -> rc.Reply boardState.Get
      | Move (rc, move) ->
        printfn "newboard %A" move
        let oldBoard = boardState.Get
        let content = Piece(Rook White)
        let newBoard = oldBoard.Add(move.To, content)
        let newBoardState = boardState.Add newBoard
        rc.Reply newBoardState
        loop (command::commandHistory)
      | SelectPiece (rc, id) ->
        rc.Reply (boardState.Get, Piece.getAllValidMoves id boardState.Get)
      return! loop commandHistory
    }
    loop []
    )
  member this.Command command = mailbox.PostAndReply (fun channel -> command(channel))
  member this.GetBoard = mailbox.PostAndReply (fun channel -> GetBoard(channel))
  member this.Move move = mailbox.PostAndReply (fun channel -> Move(channel, move))
  member this.SelectPiece id = mailbox.PostAndReply (fun channel -> SelectPiece(channel, id))

// todo boardstate vs board, vad är skillnaden osv

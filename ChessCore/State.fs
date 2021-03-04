module ChessCore.State

open ChessCore.Board
open ChessCore.Domain
type gameAgent (init) =
  let handleCommand command = ()
  let mailbox = MailboxProcessor.Start(fun inbox ->
    let rec loop (state) = async {
      let! command = inbox.Receive()
      let newState = handleCommand command
      return! loop newState
    }
    loop init
    )
  member this.Command command = mailbox.PostAndReply (fun channel -> command(channel, id))
  
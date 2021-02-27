namespace Core

module Board =
  type State () =
    let agent = MailboxProcessor.Start(fun inbox ->
      let rec messageLoop () = async{
        let! msg = inbox.Receive()
        printfn "%A" msg
        return! messageLoop ()
      }
      messageLoop ()
      )
    member this.Log msg = agent.Post msg
module Api =
  let GetBoardAgent = new Board.State()
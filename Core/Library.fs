namespace Chess

open System
open System.Security.Cryptography

module Domain =
  type Position = int
  type Color = | White | Black
  type PieceType =
    | Pawn
    | Bishop
    | Knight
    | Rook
    | Queen
    | King
  type Command =
    | LogText of string
    | PrintHistory
    | GetHistory of AsyncReplyChannel<Command list>
  type History = Command list
module Core =
  let ToRow index = index / 8
  let ToCol index = index % 8
  let ToIndex (row, col) = 8 * row + col
  let IndexToRowCol (index) = (index / 8, index % 8)
module State =
  open Domain
  type Agent() =
    let initState: History = []
    let mailbox = MailboxProcessor<Command>.Start(fun inbox ->
      let rec loop (oldState:History) = async {
        let! command = inbox.Receive()
        let newState =
          match command with
          | LogText msg ->
            command :: oldState
          | PrintHistory ->
            printfn "%A" oldState
            oldState
          | GetHistory channel ->
            channel.Reply(oldState)
            oldState
        return! loop newState
      }
      loop initState
      )
    member this.SendCommand cmd = mailbox.Post cmd
    member this.PostAndReply channel = mailbox.PostAndReply channel
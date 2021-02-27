// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Domain

[<EntryPoint>]
let main argv =
    let agent = Chess.State.Agent()
    agent.SendCommand (LogText "Testar")
    let huhu = agent.PostAndReply (fun replyChannel -> GetHistory(replyChannel))
    Console.ReadKey() |> ignore
    printfn "%A" huhu
    Console.ReadKey() |> ignore
    0 // return an integer exit code
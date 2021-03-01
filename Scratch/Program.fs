// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Chess
open Scratch.Extra
module Rules =
  type Move = {
    Piece: Piece
    From: Square
    To: Square
  }

[<EntryPoint>]
let main argv =
  0 // return an integer exit code
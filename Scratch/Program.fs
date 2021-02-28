// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System


[<EntryPoint>]
let main argv =
  let indicies = [1..64]
  let positions = [|
    [81..88] @
    [71..78] @
    [61..68] @
    [51..58] @
    [41..48] @
    [31..38] @
    [21..28] @
    [11..18]
  |] 
  printfn "%A" positions
  0 // return an integer exit code
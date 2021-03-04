module ChessCore.Game
open ChessCore.Domain
open ChessCore.State
open ChessCore.Fen
let Create (fen:Fen) =
  // todo converta fen till board
  let board = Fen.CreateBoard fen
  // todo skapa en mailboxprocessor
  let stateAgent = State.Agent(fen)
  
  ()
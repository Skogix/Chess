module ChessCore.FEN

open ChessCore.Domain

// https://www.chessprogramming.org/Forsyth-Edwards_Notation#FEN_Syntax
//<FEN> ::=  <Piece Placement>
//       ' ' <Side to move>
//       ' ' <Castling ability>
//       ' ' <En passant target square>
//       ' ' <Halfmove clock>
//       ' ' <Fullmove counter>
//
//<Piece Placement> ::= <rank8>'/'<rank7>'/'<rank6>'/'<rank5>'/'<rank4>'/'<rank3>'/'<rank2>'/'<rank1>
//<ranki>       ::= [<digit17>]<piece> {[<digit17>]<piece>} [<digit17>] | '8'
//<piece>       ::= <white Piece> | <black Piece>
//<digit17>     ::= '1' | '2' | '3' | '4' | '5' | '6' | '7'
//<white Piece> ::= 'P' | 'N' | 'B' | 'R' | 'Q' | 'K'
//<black Piece> ::= 'p' | 'n' | 'b' | 'r' | 'q' | 'k'
type File = {
  Piece: Piece option
}
type Rank = {
  File1: File
  File2: File
  File3: File
  File4: File
  File5: File
  File6: File
  File7: File
  File8: File
}
type CastleSide = {
  Queen: bool
  King: bool
}
type CastleRights = {
  White: CastleSide
  Black: CastleSide
}
type MoveCounter = uint
type Fen = {
  Rank1: Rank
  Rank2: Rank
  Rank3: Rank
  Rank4: Rank
  Rank5: Rank
  Rank6: Rank
  Rank7: Rank
  Rank8: Rank
  SideToMove: Color
  CastleRights: CastleRights
  EnPassant: File
  HalfMove: MoveCounter
  FullMove: MoveCounter
}


let fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1".Split ' '
let Pieces = fen.[0]
let SideToMove = fen.[1]
let CastleRights = fen.[2]
let EnPassant = fen.[3]
let HalfMove = fen.[4]
let FullMove = fen.[5]

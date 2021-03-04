###### 0.2
```
0.21 CreateFen
0.11 Domain and modules
0.10 VCS and project
```
####### 1.0
```f#
//Get a game agent
let game = Game.Create(initFen)
//Select a pieceId and get available moves
let availableMoves = game.SelectPiece XX
//Reply with a moveId and get new state
let moveReturn = game.Move availableMoves X
```
### ToDo
```
- Basic moves
- EnPassant
- Check
- Castle
- Input -> Output
- StateAgent
  - Generic agents
- Look over types, domain and general architecture
  - Refactor to use the same domain everywhere
  - Outside-in modeling
  - Use modules as domain instead of OOP-objects
- Readme and documentation
```
### Domain
```f#
// Board
type Rank = int // 1 to 8
type File = int // 1 to 8
type Id = File * Rank
type Color = White | Black
type Piece =
  | Pawn of Color
  | Bishop of Color
  | Knight of Color
  | Rook of Color
  | Queen of Color
  | King of Color
type Content =
    | Empty
    | Piece of Piece
type Square = Id * Content
type Board = Map (Id Content)
```
##### Scratch
```
.
```

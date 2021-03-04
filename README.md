###### 0.2
```
Domain
- Input Commands
- Output
- Events
- Agents
  - GameAgent
  - StateAgents
- Fen
  - rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
  - Pieces
  - TurnToMove
  - CastleRights
  - EnPassant
  - HalfMove
  - FullMove
- Board
  - Squares
    - File: 1 to 8
    - Rank: 1 to 8
    - Id: File*10+Rank, ie File 4, Rank 6 = 46
    - Content: Piece | Empty | EnPassant
  - 
```
####### 1.0
```f#
//Get a game agent
let game = Game.Create(initFen)
//Select a pieceId and get available moves
let availableMoves = game.SelectPiece XX
//Reply with a moveId and get new state
let moveReturn = game.Move availableMoves.X
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
.
```
##### Scratch
```
.
```
##### 0.1 VCS and routines
```
- Versioning with routines
  - Create README
  - Scratch and ToDo
  - Cleanup
```

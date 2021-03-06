###### 0.2
```
0.243 Diagnonal moves
0.242 Ui highlights
0.241 Straight moves
0.240 Get valid basic moves from pieces
0.231 Board positions/Ids
0.230 Agents
0.220 Basic output/UI
0.210 CreateBoard
0.110 Domain and modules
0.100 VCS and project
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
- Functions
  - getAllPossibleMoves piece
  
- Basic moves
  - All moves that can be made
    - Block / Capture
- Input -> Output
- StateAgent
  - Handle state
    - EnPassant
    - Check
    - Castle
  - Generic agents
- Create fen from state
- Look over types, domain and general architecture
  - Refactor to use the same domain everywhere
  - Outside-in modeling
  - Use modules as domain instead of OOP-objects
- Readme and documentation
```
### Domain
```f#
```
##### Scratch
```
```

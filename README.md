###### 0.2
```
0.23 Agents
0.22 Basic output/UI
0.21 CreateBoard
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
```
##### Scratch
```
```

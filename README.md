# Conway's Game of Life
## About
Well known cell automation game best described at https://www.conwaylife.com/
## Preview
![alt text](https://github.com/antony-jekov/game-of-life-tdd/blob/main/images/patterns.gif?raw=true)
## Description
C# / .NET Core implementation with xUnit

Video Process - https://youtu.be/zCICgSysF5E
## Rules
### Cells
The world in Conway's game is **2 dimentional** where each cell takes up **one space** on the grid. Cells are either `dead` or `alive`.
The grid is infinte and filled with either dead or alive cells. Each cell has exactly **8 neighbours** on each side.
### Cycles
Each cycle of the game determines the next state of the cells. There are three possible lifecycle changes that could happen with any given cell.
#### Lifecycle
1. Alive cells can **stay alive** as long as they have **not more than 3** and **not less that 2** alive cells around them.
2. Alive cells can **die** if they have **single neighbour** or if they are **overcrowded** with **more that 3 neighbours**.
3. Dead cells can go **back to life** if they have exactly **3 alive neighbours**. 
## Why
Exercising the Test Driven Development (TDD) approach in a simple environment.
Writing failing tests and resolving them with follwoing refactorings.

## What's inside
C# implementation using .NET Core including
* Rle Format Parsing
* Game of Life cycles logic
* Visual Tester
* Some example test patterns
* Unit tests in xUnit

## Usage
The implementation can best be seen in the `GameOfLife.cs` class:

    internal class GameOfLife
    {
        private readonly ISet<Cell> _field;
        private readonly LifeRunner _runner;

        public GameOfLife()
        {
            string patternData = Patterns.AlternateWichStrecher1;
            IEnumerable<Cell> pattern = new RlePattern(patternData);
            
            _field = new HashSet<Cell>(pattern);
            _runner = new LifeRunner(_field);
        }

        public IEnumerable<Cell> Cells => _field;

        internal void Tick()
        {
            _runner.RunCycle();
        }
    }
   
   
1 John 4 : 18
> Perfect love casts out all fear.

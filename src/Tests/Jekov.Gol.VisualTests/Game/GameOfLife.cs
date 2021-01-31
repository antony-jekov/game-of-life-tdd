using Jekov.Gol.Core;
using Jekov.Gol.VisualTests.Constants;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class GameOfLife
    {
        private readonly ISet<Cell> _field;
        private readonly LifeRunner _runner;

        public GameOfLife()
        {
            IEnumerable<Cell> pattern = new RlePattern(Patterns.AlternateWichStrecher1);
            pattern = PatternUtils.OriginToCenter(pattern);
            _field = new HashSet<Cell>(pattern);
            _runner = new LifeRunner(_field);
        }

        public IEnumerable<Cell> Cells => _field;

        internal void Tick()
        {
            _runner.RunCycle();
        }
    }
}
using Jekov.Gol.Core;
using Jekov.Gol.VisualTests.Constants;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class GameOfLife
    {
        private readonly ISet<Location> _field;
        private readonly LifeRunner _runner;

        public GameOfLife()
        {
            IEnumerable<Location> pattern = new RlePattern(Patterns.Lidka);
            pattern = PatternUtils.OriginToCenter(pattern);
            _field = new HashSet<Location>(pattern);
            _runner = new LifeRunner(_field);
        }

        public IEnumerable<Location> Cells => _field;

        internal void Tick()
        {
            _runner.RunCycle();
        }
    }
}
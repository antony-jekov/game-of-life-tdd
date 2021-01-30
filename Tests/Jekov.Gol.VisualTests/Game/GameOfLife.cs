using Jekov.Gol.Core;
using Jekov.Gol.VisualTests.Constants;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class GameOfLife
    {
        private readonly ISet<Location> _field;

        public GameOfLife()
        {
            IEnumerable<Location> pattern = new RlePattern(Patterns.Halfmax3);
            pattern = PatternUtils.OriginToCenter(pattern);
            _field = new HashSet<Location>(pattern);
        }

        public IEnumerable<Location> Cells => _field;

        internal void Tick()
        {
        }
    }
}
using Jekov.Gol.Core;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class GameOfLife
    {
        private readonly ISet<Location> _field;

        public GameOfLife()
        {
            _field = new HashSet<Location>();
        }

        public IEnumerable<Location> Cells => _field;

        internal void Tick()
        {
        }
    }
}
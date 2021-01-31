using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jekov.Gol.Core.Tests.Data.LifeRunner
{
    public abstract class CellsData : IEnumerable<object[]>
    {
        protected readonly Location Cell = new Location();
        protected readonly Location DistantCell = new Location(3, 3);
        protected readonly Location[] Neighbours;

        public CellsData()
        {
            Neighbours = Cell.Neighbours.ToArray();
        }

        public abstract IEnumerator<object[]> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jekov.Gol.Core.Tests.Data.LifeRunner
{
    public abstract class CellsData : IEnumerable<object[]>
    {
        protected readonly Cell Cell = new Cell();
        protected readonly Cell DistantCell = new Cell(3, 3);
        protected readonly Cell[] Neighbours;

        public CellsData() => Neighbours = Cell.Neighbours.ToArray();

        public abstract IEnumerator<object[]> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
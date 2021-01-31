using System;
using System.Collections.Generic;

namespace Jekov.Gol.Core
{
    public class LifeRunner
    {
        private ICollection<Location> _cells;

        public LifeRunner(ICollection<Location> cells)
        {
            _ = cells ?? throw new ArgumentNullException(nameof(cells));

            _cells = cells;
        }

        public void RunCycle()
        {
            var lostCells = new List<Location>();
            var newCells = new HashSet<Location>();

            foreach (var cell in _cells)
            {
                var potentialNeighbours = GetPotentialNeighbours(cell);
                foreach (var neighbour in potentialNeighbours)
                {
                    var neighboursCount = CountNeighbours(neighbour);
                    if (neighboursCount == 3)
                    {
                        newCells.Add(neighbour);
                    }
                }
            }

            foreach (var cell in _cells)
            {
                var neighboursCount = CountNeighbours(cell);
                if (neighboursCount < 2 || neighboursCount > 3)
                {
                    lostCells.Add(cell);
                }
            }

            foreach (var cell in lostCells)
            {
                _cells.Remove(cell);
            }

            foreach (var cell in newCells)
            {
                _cells.Add(cell);
            }
        }

        private IEnumerable<Location> GetPotentialNeighbours(Location cell)
        {
            foreach (var neighbour in cell.Neighbours)
            {
                if (!_cells.Contains(neighbour))
                {
                    yield return neighbour;
                }
            }
        }

        private int CountNeighbours(Location cell)
        {
            var count = 0;

            foreach (Location neighbour in cell.Neighbours)
            {
                count += _cells.Contains(neighbour) ? 1 : 0;
            }

            return count;
        }
    }
}
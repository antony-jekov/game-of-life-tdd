using System;
using System.Collections.Generic;

namespace Jekov.Gol.Core
{
    public class LifeRunner
    {
        private readonly ICollection<Location> _cells;
        private readonly ICollection<Location> _lostCellsTemp = new List<Location>();
        private readonly ICollection<Location> _newCellsTemp = new HashSet<Location>();
        private readonly ICollection<Location> _potentialNeighboursTemp = new HashSet<Location>();

        public LifeRunner(ICollection<Location> cells) =>
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));

        public void RunCycle()
        {
            var lostCells = CollectLostCells();
            var newCells = CollectNewCells();

            RemoveLostCells(lostCells);
            AddNewCells(newCells);
        }

        private void AddNewCells(IEnumerable<Location> newCells)
        {
            foreach (var cell in newCells)
            {
                _cells.Add(cell);
            }
        }

        private IEnumerable<Location> CollectLostCells()
        {
            _lostCellsTemp.Clear();

            foreach (var cell in _cells)
            {
                var neighboursCount = CountNeighbours(cell);
                if (neighboursCount < 2 || neighboursCount > 3)
                {
                    _lostCellsTemp.Add(cell);
                }
            }

            return _lostCellsTemp;
        }

        private IEnumerable<Location> CollectNewCells()
        {
            _newCellsTemp.Clear();
            _potentialNeighboursTemp.Clear();

            foreach (var cell in _cells)
            {
                CollectReproducedNeighbours(cell, _newCellsTemp, _potentialNeighboursTemp);
            }

            return _newCellsTemp;
        }

        private void CollectReproducedNeighbours(
            Location cell, ICollection<Location> newCells, ICollection<Location> checkedCells)
        {
            var potentialNeighbours = GetPotentialNeighbours(cell);

            foreach (var neighbour in potentialNeighbours)
            {
                if (checkedCells.Contains(neighbour))
                {
                    continue;
                }

                checkedCells.Add(neighbour);
                var neighboursCount = CountNeighbours(neighbour);
                if (neighboursCount == 3)
                {
                    newCells.Add(neighbour);
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

        private void RemoveLostCells(IEnumerable<Location> lostCells)
        {
            foreach (var cell in lostCells)
            {
                _cells.Remove(cell);
            }
        }
    }
}
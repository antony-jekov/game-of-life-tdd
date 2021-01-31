using System;
using System.Collections.Generic;

namespace Jekov.Gol.Core
{
    public class LifeRunner
    {
        private readonly ICollection<Cell> _cells;
        private readonly ICollection<Cell> _lostCellsTemp = new List<Cell>();
        private readonly ICollection<Cell> _newCellsTemp = new HashSet<Cell>();
        private readonly ICollection<Cell> _checkedNeighboursTemp = new HashSet<Cell>();

        public LifeRunner(ICollection<Cell> cells) =>
            _cells = cells ?? throw new ArgumentNullException(nameof(cells));

        public void RunCycle()
        {
            var lostCells = CollectLostCells();
            var newCells = CollectNewCells();

            RemoveLostCells(lostCells);
            AddNewCells(newCells);
        }

        private void AddNewCells(IEnumerable<Cell> newCells)
        {
            foreach (var cell in newCells)
            {
                _cells.Add(cell);
            }
        }

        private IEnumerable<Cell> CollectLostCells()
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

        private IEnumerable<Cell> CollectNewCells()
        {
            _newCellsTemp.Clear();
            _checkedNeighboursTemp.Clear();

            foreach (var cell in _cells)
            {
                CollectReproducedNeighbours(cell, _newCellsTemp, _checkedNeighboursTemp);
            }

            return _newCellsTemp;
        }

        private void CollectReproducedNeighbours(
            Cell cell, ICollection<Cell> newCells, ICollection<Cell> checkedCells)
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

        private int CountNeighbours(Cell cell)
        {
            var count = 0;

            foreach (Cell neighbour in cell.Neighbours)
            {
                count += _cells.Contains(neighbour) ? 1 : 0;
            }

            return count;
        }

        private IEnumerable<Cell> GetPotentialNeighbours(Cell cell)
        {
            foreach (var neighbour in cell.Neighbours)
            {
                if (!_cells.Contains(neighbour))
                {
                    yield return neighbour;
                }
            }
        }

        private void RemoveLostCells(IEnumerable<Cell> lostCells)
        {
            foreach (var cell in lostCells)
            {
                _cells.Remove(cell);
            }
        }
    }
}
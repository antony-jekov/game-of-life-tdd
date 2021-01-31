using Jekov.Gol.Core;
using System;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class Renderer
    {
        private const char AliveCellChar = 'X';
        private const char ClearChar = ' ';

        private readonly Cell _origin;
        private readonly int _height;
        private readonly int _width;

        private HashSet<Cell> _previous =
            new HashSet<Cell>();

        public Renderer(int width, int height, Cell origin = default)
        {
            _width = width;
            _height = height;
            _origin = origin;

            Console.SetBufferSize(_width, _height);
            Console.CursorVisible = false;
        }

        internal void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        internal void Draw(IEnumerable<Cell> cells)
        {
            var dirtyCells = new List<Cell>();

            foreach (var cell in cells)
            {
                var location = cell + _origin;
                if (IsContained(location))
                {
                    DrawCell(location, AliveCellChar);
                    dirtyCells.Add(location);
                    _previous.Remove(location);
                }
            }

            CleanDirty();

            _previous = new HashSet<Cell>(dirtyCells);
        }

        private static void DrawCell(Cell location, char cell)
        {
            Console.SetCursorPosition(location.X, location.Y);
            Console.Write(cell);
        }

        private void CleanDirty()
        {
            foreach (var cell in _previous)
            {
                DrawCell(cell, ClearChar);
            }

            _previous.Clear();
        }

        private bool IsContained(Cell cell) =>
            cell.X >= 0 && cell.X < _width &&
            cell.Y >= 0 && cell.Y < _height;
    }
}
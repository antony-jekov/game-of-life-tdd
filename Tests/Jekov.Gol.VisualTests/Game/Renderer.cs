using Jekov.Gol.Core;
using System;
using System.Collections.Generic;

namespace Jekov.Gol.VisualTests.Game
{
    internal class Renderer
    {
        private const char AliveCellChar = 'X';
        private const char ClearChar = ' ';

        private readonly Location _origin;
        private readonly int _height;
        private readonly int _width;

        private HashSet<Location> _previous =
            new HashSet<Location>();

        public Renderer(int width, int height, Location origin = default)
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

        internal void Draw(IEnumerable<Location> cells)
        {
            var dirtyCells = new List<Location>();

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

            _previous = new HashSet<Location>(dirtyCells);
        }

        private static void DrawCell(Location location, char cell)
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

        private bool IsContained(Location location) =>
            location.X >= 0 && location.X < _width &&
            location.Y >= 0 && location.Y < _height;
    }
}
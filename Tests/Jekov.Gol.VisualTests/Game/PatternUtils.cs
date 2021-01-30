using Jekov.Gol.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jekov.Gol.VisualTests.Game
{
    internal class PatternUtils
    {
        internal static IEnumerable<Location> OriginToCenter(IEnumerable<Location> pattern)
        {
            var cells = pattern.ToArray();

            int maxX = int.MinValue;
            int maxY = int.MinValue;
            int minX = int.MaxValue;
            int minY = int.MaxValue;

            foreach (var cell in cells)
            {
                maxX = Math.Max(maxX, cell.X);
                maxY = Math.Max(maxY, cell.Y);
                minX = Math.Min(minX, cell.X);
                minY = Math.Min(minY, cell.Y);
            }

            var width = (maxX - minX) + 1;
            var height = (maxY - minY) + 2;

            var offset = new Location(width / 2, height / 2);

            foreach (var cell in cells)
            {
                yield return cell - offset;
            }
        }
    }
}
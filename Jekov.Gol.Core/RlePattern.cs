using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jekov.Gol.Core
{
    public class RlePattern : IEnumerable<Location>
    {
        private Regex _patternSchema = new Regex("^([0-9]*[b|o])[0-9ob$]*!$");
        private Location[] _cells;

        public RlePattern(string patternData)
        {
            _ = patternData ?? throw new ArgumentNullException(nameof(patternData));

            if (!_patternSchema.IsMatch(patternData))
            {
                throw new DataFormatException();
            }

            _cells = BuildPattern(patternData);
        }

        private Location[] BuildPattern(string patternData)
        {
            var cells = new List<Location>();
            var digits = "";
            var x = 0;
            var y = 0;

            foreach (var ch in patternData)
            {
                if (char.IsDigit(ch))
                {
                    digits += ch;
                    continue;
                }

                var repeat = 1;
                if (digits.Length > 0)
                {
                    repeat = int.Parse(digits);
                    digits = "";
                }

                if (ch == '$')
                {
                    y += repeat;
                    x = 0;
                }

                if (ch == 'b')
                {
                    x += repeat;
                }

                if (ch == 'o')
                {
                    for (int i = 0; i < repeat; i++)
                    {
                        cells.Add(new Location(x++, y));
                    }
                }
                
            }

            return cells.ToArray();
        }

        public IEnumerator<Location> GetEnumerator()
        {
            foreach (var cell in _cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public class DataFormatException : FormatException
        {
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Jekov.Gol.Core
{
    public class RlePattern : IEnumerable<Location>
    {
        private const char NewLine = '$';
        private const char Value = 'b';

        private readonly ICollection<Location> _cells;
        private readonly Regex _cellSchema = new Regex("[0-9]*[o|b]");
        private readonly Regex _digitsSchema = new Regex(@"\d+");
        private readonly Regex _lineSchema = new Regex(@"([0-9]*\$)|([0-9ob]*[o|b])");
        private readonly Regex _patternSchema = new Regex("^([0-9]*[b|o])[0-9ob$]*!$");

        public RlePattern(string patternData)
        {
            _ = patternData ?? throw new ArgumentNullException(nameof(patternData));

            if (!_patternSchema.IsMatch(patternData))
            {
                throw new DataFormatException();
            }

            _cells = BuildPattern(patternData);
        }

        public IEnumerator<Location> GetEnumerator()
        {
            foreach (var cell in _cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<Location> BuildLine(string line, int y)
        {
            var x = 0;
            var cells = _cellSchema.Matches(line);

            foreach (Match match in cells)
            {
                var cell = match.Value;
                var repeat = ExtractRepeat(cell, 1);

                if (cell.EndsWith(Value))
                {
                    x += repeat;
                    continue;
                }

                for (int i = 0; i < repeat; i++)
                {
                    yield return new Location(x++, y);
                }
            }
        }

        private ICollection<Location> BuildPattern(string patternData)
        {
            var cells = new List<Location>();
            var lines = _lineSchema.Matches(patternData);
            var y = 0;

            foreach (Match match in lines)
            {
                var line = match.Value;

                if (line.EndsWith(NewLine))
                {
                    var repeat = ExtractRepeat(line, 1);
                    y += repeat;

                    continue;
                }

                cells.AddRange(BuildLine(line, y));
            }

            return cells;
        }

        private int ExtractRepeat(string line, int defaultValue)
        {
            var digits = _digitsSchema.Match(line);

            return digits.Success ? int.Parse(digits.Value) : defaultValue;
        }

        public class DataFormatException : FormatException
        {
        }
    }
}
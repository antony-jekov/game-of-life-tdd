using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Jekov.Gol.Core
{
    public class RlePattern : IEnumerable<Cell>
    {
        private const char NewLine = '$';
        private const char BlankCell = 'b';

        private readonly ICollection<Cell> _cells;
        private readonly Regex _cellSchema = new Regex("[0-9]*[o|b]");
        private readonly Regex _digitsSchema = new Regex(@"\d+");
        private readonly Regex _lineSchema = new Regex(@"([0-9]*\$)|([0-9ob]*[o|b])");
        private readonly Regex _patternSchema = new Regex("^([0-9]*[b|o])[0-9ob$]*!$");

        public RlePattern(string patternData)
        {
            _ = patternData ?? throw new ArgumentNullException(nameof(patternData));

            if (!_patternSchema.IsMatch(patternData))
            {
                throw new DataFormatException(patternData);
            }

            _cells = BuildPattern(patternData);
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            foreach (var cell in _cells)
            {
                yield return cell;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<Cell> BuildLine(string line, int y)
        {
            var x = 0;
            var cells = _cellSchema.Matches(line);

            foreach (Match match in cells)
            {
                var cell = match.Value;
                var repeat = ExtractNumber(cell, 1);

                if (cell.EndsWith(BlankCell))
                {
                    x += repeat;
                    continue;
                }

                for (int i = 0; i < repeat; i++)
                {
                    yield return new Cell(x++, y);
                }
            }
        }

        private ICollection<Cell> BuildPattern(string patternData)
        {
            var cells = new List<Cell>();
            var lines = _lineSchema.Matches(patternData);
            var y = 0;

            foreach (Match match in lines)
            {
                var line = match.Value;

                if (line.EndsWith(NewLine))
                {
                    var repeat = ExtractNumber(line, 1);
                    y += repeat;

                    continue;
                }

                cells.AddRange(BuildLine(line, y));
            }

            return cells;
        }

        private int ExtractNumber(string textWithNumber, int fallbackValue)
        {
            var digits = _digitsSchema.Match(textWithNumber);
            if (int.TryParse(digits.Value, out var number))
            {
                return number;
            }

            return fallbackValue;
        }

        public class DataFormatException : FormatException
        {
            public DataFormatException(string data)
                : base($"unexpected pattern format: {data}")
            {
            }
        }
    }
}
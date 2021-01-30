using Jekov.Gol.Core.Tests.Data;
using System;
using System.Linq;
using Xunit;

namespace Jekov.Gol.Core.Tests
{
    public class RlePatternTests
    {
        [Fact]
        public void NullData_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new RlePattern(null);
            });
        }

        [Theory]
        [ClassData(typeof(InvalidData))]
        public void InvalidData_ShouldThrow(string data)
        {
            Assert.Throws<RlePattern.DataFormatException>(() =>
            {
                new RlePattern(data);
            });
        }

        [Theory]
        [ClassData(typeof(BlankCells))]
        public void BlankCellsData_ShouldBeEmpty(string data)
        {
            var pattern = new RlePattern(data);

            Assert.Empty(pattern);
        }

        [Theory]
        [ClassData(typeof(AliveCells))]
        public void AliveCellsData_ShouldNotBeEmpty(string data)
        {
            var pattern = new RlePattern(data);

            Assert.NotEmpty(pattern);
        }

        [Theory]
        [ClassData(typeof(AliveCellsWithCount))]
        public void AliveCellsData_ShouldHaveProperCount(string data, int count)
        {
            var pattern = new RlePattern(data);

            Assert.Equal(count, pattern.Count());
        }

        [Theory]
        [ClassData(typeof(AliveCellsWithLocation))]
        public void AliveCellsData_ShouldHaveProperLocation(string data, int x, int y)
        {
            var pattern = new RlePattern(data);

            Assert.Equal(new Location(x, y), pattern.Last());
        }
    }
}
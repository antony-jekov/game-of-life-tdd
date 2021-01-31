using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jekov.Gol.Core.Tests
{
    public class CellTests
    {
        [Fact]
        public void EmptyConstructor_ShouldDefaultToZero()
        {
            var cell = new Cell();

            Assert.Equal(0, cell.X);
            Assert.Equal(0, cell.Y);
        }

        [Theory]
        [InlineData(2, 5)]
        [InlineData(5, 2)]
        [InlineData(0, 5)]
        [InlineData(5, 0)]
        [InlineData(int.MaxValue, int.MaxValue)]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(int.MaxValue, int.MinValue)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void ConstructorWithValues_ValuesShouldBeStoredAndBeEqual(int x, int y)
        {
            var cell = new Cell(x, y);

            Assert.Equal(x, cell.X);
            Assert.Equal(y, cell.Y);
            Assert.Equal(new Cell(x, y), cell);
            Assert.True(new Cell(x, y).Equals(cell));
            Assert.True(new Cell(x, y) == cell);
        }

        [Theory]
        [InlineData(2, 5, 7, 10)]
        [InlineData(2, 2, 5, 5)]
        [InlineData(5, 2, 5, 5)]
        [InlineData(5, 2, 2, 5)]
        [InlineData(2, 5, 5, 5)]
        [InlineData(2, 5, 5, 2)]
        public void ConstructorWithValues_ShouldNotBeEqual(int x, int y, int otherX, int otherY)
        {
            var cell = new Cell(x, y);

            Assert.NotEqual(new Cell(otherX, otherY), cell);
            Assert.False(new Cell(otherX, otherY).Equals(cell));
            Assert.False(new Cell(otherX, otherY) == cell);
            Assert.True(new Cell(otherX, otherY) != cell);
        }

        [Theory]
        [InlineData(2, 5, 7, 10, 9, 15)]
        [InlineData(2, 2, 5, 5, 7, 7)]
        [InlineData(5, 2, 5, 5, 10, 7)]
        [InlineData(5, 2, 2, 5, 7, 7)]
        [InlineData(2, 5, 5, 5, 7, 10)]
        [InlineData(2, 5, 5, 2, 7, 7)]
        [InlineData(-2, 5, -5, -2, -7, 3)]
        public void Addition_ShouldWork(
            int x, int y, int otherX, int otherY, int resultX, int resultY)
        {
            var result = new Cell(x, y) + new Cell(otherX, otherY);

            Assert.Equal(new Cell(resultX, resultY), result);
        }

        [Theory]
        [InlineData(2, 5, 7, 10, -5, -5)]
        [InlineData(2, 2, 5, 5, -3, -3)]
        [InlineData(5, 2, 5, 5, 0, -3)]
        [InlineData(5, 2, 2, 5, 3, -3)]
        [InlineData(2, 5, 5, 5, -3, 0)]
        [InlineData(2, 5, 5, 2, -3, 3)]
        [InlineData(-2, 5, -5, -2, 3, 7)]
        public void Substraction_ShouldWork(
            int x, int y, int otherX, int otherY, int resultX, int resultY)
        {
            var result = new Cell(x, y) - new Cell(otherX, otherY);

            Assert.Equal(new Cell(resultX, resultY), result);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(0, -1)]
        [InlineData(-1, 0)]
        [InlineData(1, 0)]
        [InlineData(0, 1)]
        public void Location_ShouldHaveEightNeighbours(int x, int y)
        {
            var neighbours = new Cell(x, y).Neighbours;

            Assert.Equal(8, neighbours.Count());
        }

        [Fact]
        public void Location_ShouldHaveSurroundingNeighbours()
        {
            var cell = new Cell();

            Assert.Equal(new Cell(-1, 0), cell.Left);
            Assert.Equal(new Cell(-1, -1), cell.TopLeft);
            Assert.Equal(new Cell(1, -1), cell.TopRight);
            Assert.Equal(new Cell(1, 0), cell.Right);
            Assert.Equal(new Cell(1, 1), cell.BottomRight);
            Assert.Equal(new Cell(0, 1), cell.Bottom);
            Assert.Equal(new Cell(-1, 1), cell.BottomLeft);
        }

        [Fact]
        public void LocationNeighbour_ShouldReturnSurroundingNeighbours()
        {
            var cell = new Cell();
            var neighbours = cell.Neighbours;
            var expectedNeighbours = new HashSet<Cell>
            {
                cell.Left, cell.TopLeft, cell.Top, cell.TopRight,
                cell.Right, cell.BottomRight, cell.Bottom, cell.BottomLeft
            };

            foreach (var neighbour in neighbours)
            {
                Assert.Contains(neighbour, expectedNeighbours);
            }
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, -1)]
        [InlineData(1, 1, -1)]
        [InlineData(1, -1, -1)]
        [InlineData(-1, -1, 1)]
        public void LocationCompare_ShouldCompare(int x, int y, int compare)
        {
            Cell cell = default;
            var other = new Cell(x, y);

            Assert.Equal(compare, cell.CompareTo(other));
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        public void LocationHashtag_ShouldNotBeTheSame(int x, int y)
        {
            Cell cell = new Cell(1, 2);
            var other = new Cell(x, y);

            Assert.NotEqual(cell.GetHashCode(), other.GetHashCode());
        }

        [Fact]
        public void LocationHashtag_ShouldBeTheSame()
        {
            Cell cell = new Cell(1, 2);
            var other = new Cell(1, 2);

            Assert.Equal(cell.GetHashCode(), other.GetHashCode());
        }
    }
}
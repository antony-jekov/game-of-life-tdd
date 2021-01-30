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
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("!")]
        [InlineData("o")]
        [InlineData("b")]
        [InlineData("2")]
        [InlineData("$")]
        [InlineData("22")]
        [InlineData("$o!")]
        [InlineData("!o!")]
        [InlineData("!o")]
        [InlineData("2!")]
        [InlineData("$!")]
        [InlineData("oz!")]
        [InlineData("22o")]
        [InlineData("22bo")]
        [InlineData("!ob")]
        [InlineData("!2obo")]
        public void InvalidData_ShouldThrow(string data)
        {
            Assert.Throws<RlePattern.DataFormatException>(() =>
            {
                new RlePattern(data);
            });
        }

        [Theory]
        [InlineData("b!")]
        [InlineData("bb!")]
        [InlineData("2b!")]
        [InlineData("22b!")]
        [InlineData("22b$b!")]
        [InlineData("22b$b2b!")]
        [InlineData("22b$b2b22b!")]
        public void BlankCellsData_ShouldBeEmpty(string data)
        {
            var pattern = new RlePattern(data);

            Assert.Empty(pattern);
        }

        [Theory]
        [InlineData("o!")]
        [InlineData("2o!")]
        [InlineData("2bo!")]
        [InlineData("bo!")]
        [InlineData("b$o!")]
        [InlineData("b$2bo!")]
        [InlineData("bo$2b!")]
        [InlineData("b2o$2b!")]
        [InlineData("b2$2b22$o!")]
        public void AliveCellsData_ShouldNotBeEmpty(string data)
        {
            var pattern = new RlePattern(data);

            Assert.NotEmpty(pattern);
        }

        [Theory]
        [InlineData("o!", 1)]
        [InlineData("oo!", 2)]
        [InlineData("boo!", 2)]
        [InlineData("bob!", 1)]
        [InlineData("bob$obo!", 3)]
        [InlineData("bob$obo$o22$o!", 5)]
        [InlineData("2o!", 2)]
        [InlineData("22o!", 22)]
        [InlineData("2bo!", 1)]
        [InlineData("2bo$o$2o2$o!", 5)]
        public void AliveCellsData_ShouldHaveProperCount(string data, int count)
        {
            var pattern = new RlePattern(data);

            Assert.Equal(count, pattern.Count());
        }

        [Theory]
        [InlineData("o!", 0, 0)]
        [InlineData("ooo!", 2, 0)]
        [InlineData("2bo!", 2, 0)]
        [InlineData("22b2o!", 23, 0)]
        [InlineData("22b2obo!", 25, 0)]
        [InlineData("22b2ob2o!", 26, 0)]
        [InlineData("22b2ob22o!", 46, 0)]
        [InlineData("o$o!", 0, 1)]
        [InlineData("o$bo!", 1, 1)]
        [InlineData("o$2bo!", 2, 1)]
        [InlineData("o$2bo$o!", 0, 2)]
        [InlineData("o$2bo$o$o!", 0, 3)]
        [InlineData("o$2bo$o$o2$o!", 0, 5)]
        [InlineData("o$2bo$o$o22$o!", 0, 25)]
        [InlineData("o$2bo$o$o22$2o!", 1, 25)]
        public void AliveCellsData_ShouldHaveProperLocation(string data, int x, int y)
        {
            var pattern = new RlePattern(data);

            Assert.Equal(new Location(x, y), pattern.Last());
        }
    }
}
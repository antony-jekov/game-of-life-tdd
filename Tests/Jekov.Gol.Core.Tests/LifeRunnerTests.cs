using Jekov.Gol.Core.Tests.Data.LifeRunner;
using System;
using System.Collections.Generic;
using Xunit;

namespace Jekov.Gol.Core.Tests
{
    public class LifeRunnerTests
    {
        #region Ctor

        [Fact]
        public void NullCellsList_ShouldThrow()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LifeRunner(null);
            });
        }

        #endregion Ctor

        #region Survival

        [Fact]
        public void SingleCell_ShouldNotSurvive()
        {
            var cell = new Location();
            var cells = new HashSet<Location> { cell };
            var runner = new LifeRunner(cells);

            runner.RunCycle();

            Assert.DoesNotContain(cell, cells);
        }

        [Theory]
        [ClassData(typeof(SingleNeighbourEachSide))]
        [ClassData(typeof(TwoCellsAppart))]
        [ClassData(typeof(ThreeCellsAppart))]
        [ClassData(typeof(FourNeighboursEachSide))]
        [ClassData(typeof(FiveNeighboursEachSide))]
        [ClassData(typeof(SixNeighboursEachSide))]
        [ClassData(typeof(SevenNeighboursEachSide))]
        [ClassData(typeof(EightNeighbours))]
        public void TooLittleTooFarAwayOrTooManyNeighbours_ShouldNotSurvive(
            Location cell, IEnumerable<Location> neighbours)
        {
            var cells = new HashSet<Location>(neighbours) { cell };
            var runner = new LifeRunner(cells);

            runner.RunCycle();

            Assert.DoesNotContain(cell, cells);
        }

        [Theory]
        [ClassData(typeof(TwoNeighboursEachSide))]
        [ClassData(typeof(ThreeNeighboursEachSide))]
        public void EnougNeighbours_ShouldSurvive(
            Location cell, IEnumerable<Location> neighbours)
        {
            var cells = new HashSet<Location>(neighbours) { cell };
            var runner = new LifeRunner(cells);

            runner.RunCycle();

            Assert.Contains(cell, cells);
        }

        #endregion Survival

        #region Reproduction

        [Theory]
        [ClassData(typeof(SingleNeighbourEachSide))]
        [ClassData(typeof(TwoNeighboursEachSide))]
        [ClassData(typeof(FourNeighboursEachSide))]
        [ClassData(typeof(FiveNeighboursEachSide))]
        [ClassData(typeof(SixNeighboursEachSide))]
        [ClassData(typeof(SevenNeighboursEachSide))]
        [ClassData(typeof(EightNeighbours))]
        public void TooLittleNeighbours_ShouldNotReproduce(
            Location cell, IEnumerable<Location> neighbours)
        {
            var cells = new HashSet<Location>(neighbours);
            var runner = new LifeRunner(cells);

            runner.RunCycle();

            Assert.DoesNotContain(cell, cells);
        }

        [Theory]
        [ClassData(typeof(ThreeNeighboursEachSide))]
        public void EnoughNeighbours_ShouldReproduce(
            Location cell, IEnumerable<Location> neighbours)
        {
            var cells = new HashSet<Location>(neighbours);
            var runner = new LifeRunner(cells);

            runner.RunCycle();

            Assert.Contains(cell, cells);
        }

        #endregion Reproduction
    }
}
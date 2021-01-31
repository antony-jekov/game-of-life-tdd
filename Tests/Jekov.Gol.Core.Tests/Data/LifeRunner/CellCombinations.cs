using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data.LifeRunner
{
    public class CellCombinations : CellsData
    {
        private readonly int _length;
        private readonly bool _apart;

        public CellCombinations(int length, bool apart = false) =>
            (_length, _apart) = (length, apart);

        public override IEnumerator<object[]> GetEnumerator()
        {
            var combinations = Util.GetKCombs(Neighbours, _length);

            foreach (var combo in combinations)
            {
                yield return new object[] { _apart ? DistantCell : Cell, combo };
            }
        }
    }

    public class TwoNeighboursEachSide : CellCombinations
    {
        public TwoNeighboursEachSide()
            : base(2)
        {
        }
    }

    public class ThreeNeighboursEachSide : CellCombinations
    {
        public ThreeNeighboursEachSide()
            : base(3)
        {
        }
    }

    public class FourNeighboursEachSide : CellCombinations
    {
        public FourNeighboursEachSide()
            : base(4)
        {
        }
    }

    public class FiveNeighboursEachSide : CellCombinations
    {
        public FiveNeighboursEachSide()
            : base(5)
        {
        }
    }

    public class SixNeighboursEachSide : CellCombinations
    {
        public SixNeighboursEachSide()
            : base(6)
        {
        }
    }

    public class SevenNeighboursEachSide : CellCombinations
    {
        public SevenNeighboursEachSide()
            : base(7)
        {
        }
    }
}
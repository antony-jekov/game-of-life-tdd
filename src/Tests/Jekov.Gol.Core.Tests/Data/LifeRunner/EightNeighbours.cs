using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data.LifeRunner
{
    public class EightNeighbours : CellsData
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Cell, Neighbours };
        }
    }
}
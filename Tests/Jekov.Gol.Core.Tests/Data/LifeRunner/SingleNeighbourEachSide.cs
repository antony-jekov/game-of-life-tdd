using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data.LifeRunner
{
    public class SingleNeighbourEachSide : CellsData
    {
        public override IEnumerator<object[]> GetEnumerator()
        {
            foreach (var neighbour in Neighbours)
            {
                yield return new object[] { Cell, new Location[] { neighbour } };
            }
        }
    }
}
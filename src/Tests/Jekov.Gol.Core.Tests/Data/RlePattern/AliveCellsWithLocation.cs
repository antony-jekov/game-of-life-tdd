using System.Collections;
using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data
{
    public class AliveCellsWithLocation : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "o!", 0, 0 };
            yield return new object[] { "ooo!", 2, 0 };
            yield return new object[] { "2bo!", 2, 0 };
            yield return new object[] { "22b2o!", 23, 0 };
            yield return new object[] { "22b2obo!", 25, 0 };
            yield return new object[] { "22b2ob2o!", 26, 0 };
            yield return new object[] { "22b2ob22o!", 46, 0 };
            yield return new object[] { "o$o!", 0, 1 };
            yield return new object[] { "o$bo!", 1, 1 };
            yield return new object[] { "o$2bo!", 2, 1 };
            yield return new object[] { "o$2bo$o!", 0, 2 };
            yield return new object[] { "o$2bo$o$o!", 0, 3 };
            yield return new object[] { "o$2bo$o$o2$o!", 0, 5 };
            yield return new object[] { "o$2bo$o$o22$o!", 0, 25 };
            yield return new object[] { "o$2bo$o$o22$2o!", 1, 25 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
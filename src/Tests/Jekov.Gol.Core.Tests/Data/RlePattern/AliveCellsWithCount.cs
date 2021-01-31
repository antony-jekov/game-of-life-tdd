using System.Collections;
using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data
{
    public class AliveCellsWithCount : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "o!", 1 };
            yield return new object[] { "oo!", 2 };
            yield return new object[] { "boo!", 2 };
            yield return new object[] { "bob!", 1 };
            yield return new object[] { "bob$obo!", 3 };
            yield return new object[] { "bob$obo$o22$o!", 5 };
            yield return new object[] { "2o!", 2 };
            yield return new object[] { "22o!", 22 };
            yield return new object[] { "2bo!", 1 };
            yield return new object[] { "2bo$o$2o2$o!", 5 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
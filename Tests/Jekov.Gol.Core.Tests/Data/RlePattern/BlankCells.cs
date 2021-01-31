using System.Collections;
using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data
{
    public class BlankCells : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "b!" };
            yield return new object[] { "bb!" };
            yield return new object[] { "2b!" };
            yield return new object[] { "22b!" };
            yield return new object[] { "22b$b!" };
            yield return new object[] { "22b$b2b!" };
            yield return new object[] { "22b$b2b22b!" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
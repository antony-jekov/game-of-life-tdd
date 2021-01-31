using System.Collections;
using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data
{
    public class AliveCells : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "o!" };
            yield return new object[] { "2o!" };
            yield return new object[] { "2bo!" };
            yield return new object[] { "bo!" };
            yield return new object[] { "b$o!" };
            yield return new object[] { "b$2bo!" };
            yield return new object[] { "bo$2b!" };
            yield return new object[] { "b2o$2b!" };
            yield return new object[] { "b2$2b22$o!" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
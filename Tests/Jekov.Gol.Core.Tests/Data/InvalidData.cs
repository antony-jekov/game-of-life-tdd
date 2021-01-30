using System.Collections;
using System.Collections.Generic;

namespace Jekov.Gol.Core.Tests.Data
{
    public class InvalidData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "" };
            yield return new object[] { " " };
            yield return new object[] { "!" };
            yield return new object[] { "o" };
            yield return new object[] { "b" };
            yield return new object[] { "2" };
            yield return new object[] { "$" };
            yield return new object[] { "22" };
            yield return new object[] { "$o!" };
            yield return new object[] { "!o!" };
            yield return new object[] { "!o" };
            yield return new object[] { "2!" };
            yield return new object[] { "$!" };
            yield return new object[] { "oz!" };
            yield return new object[] { "22o" };
            yield return new object[] { "22bo" };
            yield return new object[] { "!ob" };
            yield return new object[] { "!2obo" };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
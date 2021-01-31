using System;
using System.Collections.Generic;
using System.Linq;

namespace Jekov.Gol.Core.Tests.Data
{
    public static class Util
    {
        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(
            IEnumerable<T> list, int length) where T : IComparable<T> =>
            length == 1 ?
                list.Select(t => new T[] { t }) :
            GetKCombs(list, length - 1)
                .SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
    }
}
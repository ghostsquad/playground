//http://www.extensionmethod.net/csharp/ienumerable-t/bool-issorted-comparison-t-comparison

namespace WesToolbox.HelperClasses
{
    using System;
    using System.Collections.Generic;

    public static class Sorting
    {
        public static bool IsSorted<T>(this IEnumerable<T> @this, Comparison<T> comparison = null)
        {
            if (comparison == null)
            {
                comparison = Comparer<T>.Default.Compare;
            }

            using (IEnumerator<T> e = @this.GetEnumerator())
            {
                if (!e.MoveNext())
                {
                    return true;
                }

                T prev = e.Current;
                while (e.MoveNext())
                {
                    T current = e.Current;
                    if (comparison(prev, current) > 0)
                        return false;

                    prev = current;
                }
            }
            return true;
        }
    }
}

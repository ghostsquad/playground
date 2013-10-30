namespace Sorting
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// For the Brown/Black Hat Reference:
    /// http://www.youtube.com/watch?v=ywWBy6J5gz8
    /// </summary>
    public static class InPlaceQuickSorter
    {
        public static void InPlaceQuickSort<T>(this IList<T> collection)
        {
            InPlaceQuickSort(collection, Comparer<T>.Default);
        }

        public static void InPlaceQuickSort<T>(this IList<T> collection, Comparer<T> comparer)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (comparer == null)
            {
                throw new ArgumentNullException("comparer");
            }

            InPlaceQuickSortInternal(collection, comparer, 0, collection.Count - 1);            
        }

        private static void InPlaceQuickSortInternal<T>(IList<T> collection, Comparer<T> comparer, int indexStart, int indexEnd)
        {
            var blackHatIndex = indexStart;
            var blackHat = collection[blackHatIndex];

            var brownHatIndex = indexEnd;
            var brownHat = collection[brownHatIndex];

            while (brownHatIndex != blackHatIndex)
            {
                var comparisonResult = comparer.Compare(brownHat, blackHat);

                if (comparisonResult == 0)
                {
                    if (brownHatIndex < blackHatIndex)
                    {
                        brownHatIndex++;
                        brownHat = collection[brownHatIndex];
                    }
                    else
                    {
                        brownHatIndex--;
                        brownHat = collection[brownHatIndex];
                    }
                }
                else if (comparisonResult > 0)
                {
                    if (brownHatIndex < blackHatIndex)
                    {                        
                        collection[brownHatIndex] = blackHat;
                        collection[blackHatIndex] = brownHat;
                        var tempIndex = blackHatIndex;
                        blackHatIndex = brownHatIndex;
                        brownHatIndex = tempIndex;
                        brownHatIndex--;
                        brownHat = collection[brownHatIndex];
                    }
                    else
                    {
                        brownHatIndex--;
                        brownHat = collection[brownHatIndex];
                    }
                }
                else if (comparisonResult < 0)
                {
                    if (brownHatIndex > blackHatIndex)
                    {                        
                        collection[brownHatIndex] = blackHat;                     
                        collection[blackHatIndex] = brownHat;
                        var tempIndex = blackHatIndex;
                        blackHatIndex = brownHatIndex;
                        brownHatIndex = tempIndex;                        
                        brownHatIndex++;                        
                    }
                    else
                    {
                        brownHatIndex++;
                        brownHat = collection[brownHatIndex];
                    }
                }
                else
                {
                    break;
                }
            }

            var leftEnd = blackHatIndex - 1;
            var rightStart = blackHatIndex + 1;

            if (blackHatIndex > 0 && leftEnd >= indexStart)
            {
                InPlaceQuickSortInternal(collection, comparer, indexStart, leftEnd);
            }
            
            if (blackHatIndex < collection.Count - 1 && rightStart <= indexEnd)
            {
                InPlaceQuickSortInternal(collection, comparer, rightStart, indexEnd);
            }
        }        
    }
}

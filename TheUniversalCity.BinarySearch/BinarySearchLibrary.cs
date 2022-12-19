using System;
using TheUniversalCity.BinarySearch.Consts;

namespace TheUniversalCity.BinarySearch
{
    public static class BinarySearchLibrary
    {
        public static void FindConvergentBoundaries<T>(
            T startRange, 
            T endRange, 
            T findValue, 
            int power, 
            out T startBoundaryResult, 
            out T endBoundaryResult)
            where T : struct
        {
            dynamic value = findValue;

            if (
                !(value is byte) &&
                !(value is short) &&
                !(value is int) &&
                !(value is long) &&
                !(value is float) &&
                !(value is double))
            {
                throw new NotSupportedException($"Value: {findValue} that is type of {typeof(T)} not supported");
            }

            dynamic _startRange = startRange;
            dynamic _endRange = endRange;

            for (int i = 0; i < power; i++)
            {
                var halfRange = ((_endRange - _startRange) / 2) + _startRange;

                switch (Math.Sign(halfRange - value))
                {
                    case 0:
                    case -1:
                        _startRange = halfRange;
                        break;
                    case 1:
                        _endRange = halfRange;
                        break;
                }
            }

            startBoundaryResult = _startRange;
            endBoundaryResult = _endRange;
        }

        public static int FindNearestIndex<TEntity, TFindValue>(
    TEntity[] sortedEntityCollection,
    TFindValue findValue,
    Func<TEntity, TFindValue, ComparisonResults> comparisonLambda)
        {
            double startIndex = 0;
            double endIndex = sortedEntityCollection.Length - 1;

            while (true)
            {
                var halfIndex = (endIndex + startIndex) / 2.0;

                switch (comparisonLambda(sortedEntityCollection[(int)Math.Round(halfIndex)], findValue))
                {
                    case ComparisonResults.Equal:
                        return (int)Math.Round(halfIndex);
                    case ComparisonResults.LessThen:
                        if ((int)halfIndex == (int)startIndex)
                        {
                            return (int)Math.Round(halfIndex);
                        }

                        startIndex = halfIndex;
                        break;
                    case ComparisonResults.GreaterThen:
                        if ((int)halfIndex == (int)endIndex)
                        {
                            return (int)Math.Round(halfIndex);
                        }

                        endIndex = halfIndex;
                        break;
                }
            }
        }
    }
}

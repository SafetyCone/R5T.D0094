using System;

using R5T.D0094.A001;


namespace System
{
    public static class IServiceAggregationExtensions
    {
        public static T FillFrom<T>(this T aggregation,
            IServiceAggregation other)
            where T : IServiceAggregation
        {
            throw new NotImplementedException();
        }
    }
}
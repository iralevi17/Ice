using Ice.WeatherForecast.General.Interfaces;
using System;
using System.Collections.Generic;

namespace Ice.WeatherForecast.Service
{
    public class BucketNameIterator : IBucketNameIterator
    {
        private const int MaxOffset = 120;
        private const int HoursInADay = 24;

        public IEnumerable<(string timeStamp, string offset)> Next(DateTime bucketTimeStamp)
        {
            var bucketName = NextBucketName(bucketTimeStamp);

            do {
                var bucket = bucketName.Format();

                yield return bucket;

                bucketName.offset += HoursInADay;
                bucketName.timeStamp = bucketName.timeStamp.AddDays(-1);

            } while (bucketName.offset <= MaxOffset);
        }

        private (DateTime timeStamp, int offset) NextBucketName(DateTime bucketTimeStamp)
        {
            var result = default((DateTime timeStamp, int offset));

            result.timeStamp = bucketTimeStamp;
            result.offset = bucketTimeStamp.Hour;

            var dateTimeDiff = bucketTimeStamp - DateTime.Now;

            if (dateTimeDiff.Days > 0 || dateTimeDiff.Hours > 0)
            {
                result.timeStamp = DateTime.Now;
                result.offset = dateTimeDiff.Days * 24 + dateTimeDiff.Hours;
            }

            return result;
        }
    }
}

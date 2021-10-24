using System;

namespace Ice.WeatherForecast.Service
{
    public static class BucketNameFormatter
    {
        public static (string timeStamp, string offset) Format(this (DateTime timeStamp, int offset) bucketName)
        {
            var result = default((string timeStamp, string offset));

            result.timeStamp = bucketName.timeStamp.ToString("yyyyMMdd");
            result.offset = bucketName.offset.ToString().PadLeft(3, '0');

            return result;
        }
    }
}

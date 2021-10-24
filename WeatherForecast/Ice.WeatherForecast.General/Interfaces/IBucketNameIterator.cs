using System;
using System.Collections.Generic;

namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IBucketNameIterator
    {
        IEnumerable<(string timeStamp, string offset)> Next(DateTime bucketTimeStamp);
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IDataStorage
    {
        Task<string> DownloadTemperatureFile(DateTime bucketDateTimeName, CancellationToken cancellationToken);
    }
}

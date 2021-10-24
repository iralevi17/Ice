using System.Threading;
using System.Threading.Tasks;
using Ice.WeatherForecast.General;
using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class WeatherForecastManager : IWeatherForecastManager
    {
        private readonly IDataStorage _dataStorage;
        private readonly ICacheManager _cacheManager;
        private readonly IForecastTemperatureProvider _forecastTemperatureProvider;

        // Should be a job 
        // Responsoblity: access to local file system to monitor forecast files
        // Get last updates from s3 and sync files
        private readonly IForecastPopulator _forecastPopulator; // not implemented
                                                                
        public WeatherForecastManager(IDataStorage dataStorage, ICacheManager cacheManager, IForecastTemperatureProvider forecastTemperatureProvider, IForecastPopulator forecastPopulator)
        {
            _dataStorage = dataStorage;
            _cacheManager = cacheManager;
            _forecastTemperatureProvider = forecastTemperatureProvider;
            _forecastPopulator = forecastPopulator;
        }

        public async Task<double> GetTemperatureC (ForecastTemperatureRequest request, CancellationToken cancellationToken)
        {
            var forecastTemperature = await _cacheManager.Get(request);

            if (forecastTemperature == null)
            {
               // ForecastPopulator should check if file exists in its repository.
               // if not - file will be downloaded from AWS
               // if(!_forecastPopulator.FindFile(fileName)) --pseudo code
               //{
                var fileName = await _dataStorage.DownloadTemperatureFile(request.TimeStamp, cancellationToken);
               //}
                forecastTemperature = await _forecastTemperatureProvider.GetForecastTemperature(fileName, request.Lat, request.Lon);

                var savedToCache = await _cacheManager.Set(request, forecastTemperature);

                if(!savedToCache)
                {
                    // write error to log
                }
            }

            return forecastTemperature.TemperatureC;
        }
    }
}

using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    // Not Implemented
    // Responsoblity: access to local file system to monitor forecast files
    // Get last update from s3 and sync files
    public class ForecastPopulator : IForecastPopulator
    {
        public bool FindFile(string fileName)
        {
            return false; //mock
        }
    }
}

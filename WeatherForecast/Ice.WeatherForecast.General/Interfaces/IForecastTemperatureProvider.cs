using System.Threading.Tasks;

namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IForecastTemperatureProvider
    {
        Task<ForecastTemperature> GetForecastTemperature(string filePath, double lat, double log);
    }
}

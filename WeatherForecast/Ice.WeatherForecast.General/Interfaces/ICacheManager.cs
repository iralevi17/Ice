using System.Threading.Tasks;

namespace Ice.WeatherForecast.General.Interfaces
{
    public interface ICacheManager
    {
        Task<ForecastTemperature> Get(ForecastTemperatureRequest key);
        Task<bool> Set(ForecastTemperatureRequest key, ForecastTemperature value);
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IWeatherForecastManager
    {
        Task<double> GetTemperatureC(ForecastTemperatureRequest request, CancellationToken cancellationToken);
    }
}


namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IForecastTemperatureParser
    {
        ForecastTemperature Parse(string forecastTemperature);
    }
}

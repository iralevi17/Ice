namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IForecastTemperatureConverter
    {
        double KelvinToCelsius(double temperatureK);
    }
}

using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class ForecastTemperatureConverter : IForecastTemperatureConverter
    {
        private const float AbsoluteZero = -273.15f;
        public double KelvinToCelsius(double temperatureK) => temperatureK + AbsoluteZero;
    }
}


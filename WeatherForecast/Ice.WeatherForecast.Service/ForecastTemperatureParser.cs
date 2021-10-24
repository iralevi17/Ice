using System;
using Ice.WeatherForecast.General;
using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class ForecastTemperatureParser : IForecastTemperatureParser
    {
        private const int ChunkCount = 3;
        private readonly IForecastTemperatureConverter _forecastTemperatureConverter;

        public ForecastTemperatureParser(IForecastTemperatureConverter forecastTemperatureConverter)
        {
            _forecastTemperatureConverter = forecastTemperatureConverter;
        }

        public ForecastTemperature Parse(string forecastTemperatureK) 
        {
            try
            {
                var chunks = forecastTemperatureK.Split(',');

                if(chunks.Length != ChunkCount)
                    throw new Exception ($"Illegal forecast temperature data '{forecastTemperatureK}'");

                var values = new double[chunks.Length];

                for (var i=0; i< chunks.Length; i++)
                {
                    var index = chunks[i].IndexOf("=") + 1;
                    var value = chunks[i].Substring(index);

                    values[i] = Math.Round(double.Parse(value), 3);
                }

                var result = new ForecastTemperature
                {
                    Latitute = values[0],
                    Longitute = values[1],
                    TemperatureC = Math.Round(_forecastTemperatureConverter.KelvinToCelsius(values[2]), 3)
                };

                return result;
            }
            catch(Exception ex)
            {
                // write to log
                throw new Exception($"Failed to parse forecast temperature '{forecastTemperatureK}'", ex);
            }
        }
    }
}

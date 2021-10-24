using System;

namespace Ice.WeatherForecast.General
{
    public class ForecastTemperatureRequest
    {
        public DateTime TimeStamp { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}

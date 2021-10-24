using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Ice.WeatherForecast.General;
using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class ForecastTemperatureProvider : IForecastTemperatureProvider
    {
        private readonly IForecastTemperatureParser _forecastTemperatureParser;
        private readonly IWGrib2ToolCommandBuilder _wGrib2ToolCommandBuider;

        public ForecastTemperatureProvider(IForecastTemperatureParser forecastTemperatureParser, IWGrib2ToolCommandBuilder wGrib2Tool)
        {
            _forecastTemperatureParser = forecastTemperatureParser;
            _wGrib2ToolCommandBuider = wGrib2Tool;
        }

        public async Task<ForecastTemperature> GetForecastTemperature(string filePath, double lat, double lon)
        {
            try
            {
                var wgrib2Command = _wGrib2ToolCommandBuider.GetCommand(filePath, lat, lon);

                var procStartInfo = new ProcessStartInfo("cmd", wgrib2Command)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false
                };

                var proc = new Process
                {
                    StartInfo = procStartInfo
                };

                proc.Start();

                // ReadToEndAsync - method doesn't contain CnacellationToken param that is why it is not passed
                var forecastTemperatureOutput = await proc.StandardOutput.ReadToEndAsync(); 
                var result = _forecastTemperatureParser.Parse(forecastTemperatureOutput);

                return result;
            }
            catch (Exception ex)
            {
                // write to log
                throw new Exception($"Failed to extract forecast temperature from file '{filePath}'", ex);
            }
        }
    }
}

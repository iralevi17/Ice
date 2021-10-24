using Ice.WeatherForecast.General.Interfaces;

namespace Ice.WeatherForecast.Service
{
    public class WGrib2ToolCommandBuilder : IWGrib2ToolCommandBuilder
    {
        private const string Wgrib2CliToolPath = @"C:\Users\irale\Desktop\wgrib2\wgrib2.exe";
        private string Wgrib2CliTemplate = "/c {0} {1} -match \":(TMP:2 m above ground):\" -lon {2} {3}";

        public string GetCommand(string filePath, double lat, double lon) => string.Format(Wgrib2CliTemplate, Wgrib2CliToolPath, filePath, lat, lon);
    }
}

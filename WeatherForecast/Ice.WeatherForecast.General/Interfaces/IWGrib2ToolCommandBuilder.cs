namespace Ice.WeatherForecast.General.Interfaces
{
    public interface IWGrib2ToolCommandBuilder
    {
        string GetCommand(string filePath, double lat, double lon);
    }
}

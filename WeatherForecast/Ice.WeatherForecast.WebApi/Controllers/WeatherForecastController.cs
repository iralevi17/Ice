using Ice.WeatherForecast.General;
using Ice.WeatherForecast.General.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ice.WeatherForecast.Controllers
{
    public class WeatherForecastController : ApiController
    {
        private IWeatherForecastManager _weatherForecastManager;

        public WeatherForecastController(IWeatherForecastManager weatherForecastManager)
        {
            _weatherForecastManager = weatherForecastManager;
        }

        [HttpPost]
        [Route("weatherforecast")]
        public async Task<IHttpActionResult> GetForecastTemperatureC([FromBody]ForecastTemperatureRequest forecastTemperatureRequest, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _weatherForecastManager.GetTemperatureC(forecastTemperatureRequest, cancellationToken);

                return Ok(result);
            }
            catch(Exception ex)
            {
                // write to log
                throw ex;
            }
        }
    }
}
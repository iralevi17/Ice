using System;
using System.Threading.Tasks;
using Ice.WeatherForecast.General;
using Ice.WeatherForecast.General.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Ice.WeatherForecast.Service
{
    public class RedisCacheManager : ICacheManager
    {
        private const string ConnectionString = "localhost:7000";
        static RedisCacheManager()
        {
            _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(ConnectionString);
            });
        }

        private static Lazy<ConnectionMultiplexer> _lazyConnection;

        private static ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        public async Task<ForecastTemperature> Get(ForecastTemperatureRequest key)
        {
            var cache = Connection.GetDatabase();

            var strKey = JsonConvert.SerializeObject(key);
            var value = await cache.StringGetAsync(strKey);

            if (value.IsNullOrEmpty)
                return null;

            var strValue = value.ToString();

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
            };

            var result = JsonConvert.DeserializeObject<ForecastTemperature>(strValue, jsonSerializerSettings);

            return result;
        }

        public async Task<bool> Set(ForecastTemperatureRequest key, ForecastTemperature value)
        {
            var strKey = JsonConvert.SerializeObject(key);
            var strValue = JsonConvert.SerializeObject(value);

            var cache = Connection.GetDatabase();

            var result = await cache.StringSetAsync(strKey, strValue);

            return result;
        }
    }
}

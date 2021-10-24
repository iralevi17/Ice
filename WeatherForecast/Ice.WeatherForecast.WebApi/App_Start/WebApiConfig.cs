using Ice.WeatherForecast.General.Interfaces;
using Ice.WeatherForecast.Service;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using WebApiThrottle;

namespace Ice.WeatherForecast
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IDataStorage, AwsDataStorage>();
            container.RegisterType<ICacheManager, RedisCacheManager>();
            container.RegisterType<IBucketNameIterator, BucketNameIterator>();
            container.RegisterType<IForecastPopulator, ForecastPopulator>();
            container.RegisterType<IWGrib2ToolCommandBuilder, WGrib2ToolCommandBuilder>();
            container.RegisterType<IForecastTemperatureConverter, ForecastTemperatureConverter>(new HierarchicalLifetimeManager());
            container.RegisterType<IForecastTemperatureParser, ForecastTemperatureParser>(new HierarchicalLifetimeManager());
            container.RegisterType<IForecastTemperatureProvider, ForecastTemperatureProvider>(new HierarchicalLifetimeManager());
            container.RegisterType<IWeatherForecastManager, WeatherForecastManager>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            config.MessageHandlers.Add(new ThrottlingHandler()
            {
                Policy = new ThrottlePolicy(perSecond: 3)
                {
                    IpThrottling = true,
                    ClientThrottling = true,
                    EndpointThrottling = true
                },
                Repository = new CacheRepository()
            });

            config.MapHttpAttributeRoutes();
        }
    }
}

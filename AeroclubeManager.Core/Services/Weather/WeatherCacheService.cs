using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Weather;
using AeroclubeManager.Core.Interfaces.Services.Weather;
using Microsoft.Extensions.Caching.Memory;

namespace AeroclubeManager.Core.Services.Weather
{
    public class WeatherCacheService : IWeatherCacheService
    {
        private readonly IMemoryCache _memoryCache;

        public WeatherCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public WeatherData? GetWeatherDataCache(string id)
        {

            if(_memoryCache.TryGetValue(id, out WeatherData? weatherData))
            {
                return weatherData;
            }

               return null;
            }

            public bool SetWeatherDataCache(string id, WeatherData data)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            try
            {
                _memoryCache.Set(id, data, cacheEntryOptions);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}

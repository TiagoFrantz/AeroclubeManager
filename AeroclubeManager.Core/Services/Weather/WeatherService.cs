using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Weather;
using AeroclubeManager.Core.Interfaces.Services.Weather;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace AeroclubeManager.Core.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IWeatherCacheService _cacheService;
        private string ApiKey = string.Empty;

        public WeatherService(HttpClient httpClient, IWeatherCacheService cacheService)
        {
            _httpClient = httpClient;
            ApiKey = Environment.GetEnvironmentVariable("APIKEY_WEATHERAPI");
            _cacheService = cacheService;
        }
        public async Task<WeatherData?> GetWeatherAsync(double? latitude, double? longitude)
        {
            var url = $"http://api.weatherapi.com/v1/current.json?key={ApiKey}&q={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}";
            var result = await _httpClient.GetAsync(url);

            if (result == null || result.IsSuccessStatusCode == false)
            {
                return null;
            }

            var response = await result.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<dynamic>(response);
            var weatherData = new WeatherData();
            try
            {
                weatherData.City = resultObject.location.name;
                weatherData.Country = resultObject.location.country;
                weatherData.LastUpdated = resultObject.current.last_updated;
                weatherData.ConditionText = resultObject.current.condition.text;
                weatherData.IconUrl = resultObject.current.condition.icon;
                weatherData.TemperatureC = resultObject.current.temp_c;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao tentar converter o json em objeto: " + ex);
                return null;
            }

            return weatherData;
        }

        public async Task<WeatherData?> GetWeatherUsingCacheAsync(string icao, double? latitude, double? longitude)
        {
            var result = _cacheService.GetWeatherDataCache(icao);

                        if (result == null)
            {
                if(latitude == null || longitude == null)
                {
                    return null;
                }
                result = await GetWeatherAsync(latitude, longitude);
                _cacheService.SetWeatherDataCache(icao, result);
                return result;

            }

            return result;
        }

    }
}

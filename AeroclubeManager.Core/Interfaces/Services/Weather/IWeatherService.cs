using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Weather;

namespace AeroclubeManager.Core.Interfaces.Services.Weather
{
    public interface IWeatherService
    {


        public Task<WeatherData?> GetWeatherAsync(double?  latitude, double? longitude);
        public Task<WeatherData?> GetWeatherUsingCacheAsync(string icao, double? latitude, double? longitude);
    }
}

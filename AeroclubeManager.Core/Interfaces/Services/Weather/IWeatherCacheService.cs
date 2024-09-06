using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Weather;

namespace AeroclubeManager.Core.Interfaces.Services.Weather
{
    public interface IWeatherCacheService
    {
        /// <summary>
        /// Esse método pega o cache com base no id, que no caso é o ICAO
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Um WeatherData ou nulo caso não haver</returns>
        public WeatherData? GetWeatherDataCache(string id);


        /// <summary>
        /// Seta um valor de cacho
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns>Retorna um bool se definiu ou não</returns>
        public bool SetWeatherDataCache(string id, WeatherData data); 
    }
}

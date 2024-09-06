using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.Weather
{
    public class WeatherData : BaseEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string LastUpdated { get; set; }
        public string ConditionText { get; set; }
        public string IconUrl { get; set; }
        public string TemperatureC { get; set; }

    }
}

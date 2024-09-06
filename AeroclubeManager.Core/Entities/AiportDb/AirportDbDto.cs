using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.AiportDb
{
    public class AirportDbDto : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; } 
        public double Longitude { get; set; }
    }
}

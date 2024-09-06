using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.AiportDb;

namespace AeroclubeManager.Core.Interfaces.Services.Airport
{
    public interface IAirportDbService
    {
        public Task<AirportDbDto?> GetAirportDb(string icao);
    }
}

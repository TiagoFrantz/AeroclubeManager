using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;

namespace AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices
{
    public interface IFlightSchoolService
    {
        public Task<FlightSchool?> GetFlightSchoolById(string id);
        public Task<FlightSchool?> CreateFlightSchool(FlightSchool flightSchool);


        public Task<FlightSchool?> UpdateFlightSchool(FlightSchool flightSchool);

        public Task<bool> DeleteFlightSchoolById(string id);

        public Task<List<FlightSchool>?> GetAllFlightSchools();

        public Task<List<FlightSchool>> GetFlightSchoolsByUserId(string userId);


    }
}

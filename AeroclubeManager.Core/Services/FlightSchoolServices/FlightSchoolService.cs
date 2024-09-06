using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Interfaces.Repos.FlightSchoolRepos;
using AeroclubeManager.Core.Interfaces.Services.FlightSchoolServices;

namespace AeroclubeManager.Core.Services.FlightSchoolServices
{
    public class FlightSchoolService : IFlightSchoolService
    {

        private readonly IFlightSchoolRepository _repository;

        public FlightSchoolService(IFlightSchoolRepository repository)
        {
            _repository = repository;
        }

        public async Task<FlightSchool?> CreateFlightSchool(FlightSchool flightSchool)
        {
            return await _repository.CreateFlightSchool(flightSchool);
        }

        public async Task<bool> DeleteFlightSchoolById(string id)
        {
            var result = await _repository.DeleteFlightSchool(Guid.Parse(id));
            return result;
        }

        public async Task<List<FlightSchool>?> GetAllFlightSchools()
        {
            var result = await _repository.GetAllFlightSchools();
            return result;
        }

        public async Task<FlightSchool?> GetFlightSchoolById(string id)
        {
            var result = await _repository.GetFlightSchoolById(Guid.Parse(id));
            return result == null ? null : result;
        }

        public async Task<List<FlightSchool>> GetFlightSchoolsByUserId(string userId)
        {
            return await _repository.GetFlightSchoolsByUserId(userId);
        }

        public async Task<FlightSchool?> UpdateFlightSchool(FlightSchool flightSchool)
        {
            if(flightSchool == null)
            {
                return null;
            }

            var result = await _repository.UpdateFlightSchool(flightSchool);
            return result;
        }

    }
}

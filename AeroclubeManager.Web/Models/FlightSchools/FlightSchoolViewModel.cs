using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Web.Models.FlightSchools
{
    public class FlightSchoolViewModel
    {
        public FlightSchool FlightSchool { get; set; }
        public UserFlightSchool UserFlightSchool { get; set; }
        public string LocationName { get; set; } 
        public string LastUpdated { get; set; }
        public string ConditionText { get; set; }
        public string IconUrl { get; set; }
        public string TemperatureC {  get; set; } 
    }
}

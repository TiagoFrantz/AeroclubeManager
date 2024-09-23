using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Web.Models.Dashboard
{
    public class DashboardFlightSchoolModelView
    {
        public FlightSchool FlightSchool { get; set; }
        public UserFlightSchool UserFlightSchool { get; set; }
    }
}

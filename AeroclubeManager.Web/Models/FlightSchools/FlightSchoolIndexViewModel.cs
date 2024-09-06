using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Web.Models.FlightSchools
{
    public class FlightSchoolIndexViewModel
    {
        public ApplicationUser? User { get; set; }
        public List<FlightSchoolViewModel>? FlightSchools { get; set; }
    }
}

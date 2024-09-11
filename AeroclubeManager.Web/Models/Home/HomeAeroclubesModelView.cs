using AeroclubeManager.Core.Entities.FlightSchoolEntities;

namespace AeroclubeManager.Web.Models.Home
{
    public class HomeAeroclubesModelView
    {
        public List<FlightSchool> FlightSchools { get; set; } = new List<FlightSchool>();
        public int PageCount { get; set; }
        public int PageNumber { get; set; } = 1;
        public string? SearchString { get; set; }
    }
}

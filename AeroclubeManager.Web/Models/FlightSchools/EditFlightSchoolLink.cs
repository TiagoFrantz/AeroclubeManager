using AeroclubeManager.Core.Entities;

namespace AeroclubeManager.Web.Models.FlightSchools
{
    public class EditFlightSchoolLink : BaseEntity
    {
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}

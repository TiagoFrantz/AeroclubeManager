using System.ComponentModel.DataAnnotations;

namespace AeroclubeManager.Web.Models.FlightSchools
{
    public class EditFlightSchool
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        [Required]
        public string Description { get; set; }
        public bool UpdatedImage { get; set; } = false;
        public List<EditFlightSchoolLink> Links = new List<EditFlightSchoolLink>();
        [Required]
        public string ICAO { get; set; } = string.Empty;
        [Required]
        public string AirportName { get; set; } = string.Empty;
        [Required]
        public string AirportState {  get; set; } = string.Empty;
        [Required]
        public string AirportCity {  get; set; } = string.Empty;
        [Required]

        public string FlightSchoolDocument = string.Empty;
        [Required]

        public string FlightSchoolLicenseNumber { get; set; } = string.Empty;
    }
}

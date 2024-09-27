using System.ComponentModel.DataAnnotations;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;

namespace AeroclubeManager.Web.Models.FlightSchools
{
    public class EditFlightSchool
    {
        [MaxLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool UpdatedImage { get; set; } = false;
        [Required]
        public string ICAO { get; set; } = string.Empty;
        [Required]
        public string AirportName { get; set; } = string.Empty;
        [Required]
        public string AirportState {  get; set; } = string.Empty;
        [Required]
        public string AirportCity {  get; set; } = string.Empty;
        [Required]

        public string FlightSchoolDocument { get; set; } = string.Empty;
        [Required]

        public string FlightSchoolLicenseNumber { get; set; } = string.Empty;

        public string LinksJson {  get; set; } = string.Empty;

    }
}

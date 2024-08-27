using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.Review;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class FlightSchool : BaseEntity
    {

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;


        [MaxLength(65535)]
        [Column(TypeName = "TEXT")]
        public string Description { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? LogoUrl { get; set; } = string.Empty;

        public ICollection<Airplane> Planes { get; set; } = new List<Airplane>();

        public Airport? SchoolFlightAirport { get; set; }

        public Guid? SchoolFlightAirportId { get; set; } = Guid.Empty;


        //CNPJ?
        [MaxLength(255)]
        public string Document { get; set; } = string.Empty;


        //Codigo do CIAC
        [MaxLength(255)]
        public string LicenseNumber {  get; set; } = string.Empty;


        /// <summary>
        /// Exemplo: website, redes sociais, etc
        /// </summary>
        public ICollection<string> Links { get; set; } = new List<string>();

        public ICollection<FlightSchoolReview?> Reviews { get; set; } = new List<FlightSchoolReview>();

        public ICollection<UserFlightSchool> Users { get; set; } = new List<UserFlightSchool>();

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchool
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

    }
}

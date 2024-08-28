using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class Airport : BaseEntity
    {

        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string ICAO { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Location { get; set; } = string.Empty;


        public FlightSchool FlightSchool { get; set; }
        public Guid FlightSchoolId { get; set; } = Guid.Empty;
    }
}

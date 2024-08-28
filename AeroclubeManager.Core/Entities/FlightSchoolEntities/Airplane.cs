using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class Airplane : BaseEntity
    {
        [MaxLength(255)]
        public string Manufacturer { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Model { get; set; } = string.Empty;

        public ICollection<AirplaneImage> Images { get; set; } = new List<AirplaneImage>();

        public short Year { get; set; }

        public FlightSchool? FlightSchoolAirplane { get; set; }

        public Guid? FlightSchoolId { get; set; } = Guid.Empty;

        public AirplaneStatus Status { get; set; } = AirplaneStatus.Available;

    }
}

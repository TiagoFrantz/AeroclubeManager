using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class Flight : BaseEntity
    {
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public DateTime FlightDate { get; set; } = DateTime.UtcNow;
        public Airplane? FlightAirplane { get; set; }
        public Guid? AirplaneId { get; set; } = Guid.Empty;


        public Guid? FlightSchoolId { get; set; } = Guid.Empty;

        public FlightSchool? FlightSchool { get; set; } 


        public Guid? InstructorId { get; set; } = Guid.Empty;

        public UserFlightSchool? Instructor { get; set; }

        public Guid? StudentId {  get; set; } = Guid.Empty;
        public UserFlightSchool? Student { get; set; }

    }
}

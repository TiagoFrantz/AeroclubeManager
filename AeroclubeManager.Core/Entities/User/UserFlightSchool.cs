using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;

namespace AeroclubeManager.Core.Entities.User
{
    public class UserFlightSchool : BaseEntity
    {
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; } = Guid.Empty.ToString();

        public FlightSchool? FlightSchool { get; set; }
        public Guid? FlightSchoolId { get; set; } = Guid.Empty;

        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public ICollection<UserFlightSchoolRole> FlightSchoolRoles { get; set; } = new List<UserFlightSchoolRole>();
    }
}

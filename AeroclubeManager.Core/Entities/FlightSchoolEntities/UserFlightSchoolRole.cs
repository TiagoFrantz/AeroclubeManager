using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class UserFlightSchoolRole : BaseEntity
    {
        public FlightSchoolRoleEnum Role { get; set; }

        public UserFlightSchool User {  get; set; }
        public Guid UserId { get; set; }
    }
}

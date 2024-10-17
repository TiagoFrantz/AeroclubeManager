using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class FlightSchoolSharedLink : BaseEntity
    {
        public Guid FlightSchoolId { get; set; }
        public FlightSchool FlightSchool { get; set; }
        public string Token { get; set; }
        public FlightSchoolRoleEnum Role { get; set; }
        public int Count { get; set; }
    }
}

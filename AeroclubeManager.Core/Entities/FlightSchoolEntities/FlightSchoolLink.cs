using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class FlightSchoolLink : BaseEntity
    {
        [MaxLength(255)]
        public string Url { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Text { get; set; } = string.Empty;


        public Guid? FlightSchoolId { get; set; } = Guid.Empty;

        public FlightSchool? FlightSchool { get; set; }
    }
}

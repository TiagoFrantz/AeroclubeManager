using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroclubeManager.Core.Entities.FlightSchoolEntities
{
    public class AirplaneImage : BaseEntity
    {
        [MaxLength(255)]
        public string Url { get; set; }

        [MaxLength(255)]
        public string Text { get; set; }


        public Guid? AirplaneId { get; set; } = Guid.Empty;

        public Airplane? Airplane { get; set; }
    }
}

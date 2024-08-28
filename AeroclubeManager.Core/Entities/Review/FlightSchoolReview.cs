using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AeroclubeManager.Core.Entities.FlightSchoolEntities;
using AeroclubeManager.Core.Entities.User;

namespace AeroclubeManager.Core.Entities.Review
{
    public class FlightSchoolReview : BaseEntity
    {
        public Guid? SchoolFlightId { get; set; } = Guid.Empty;
        public FlightSchool? FlightSchool { get; set; }

        public string? UserId { get; set; } = Guid.Empty.ToString();
        public ApplicationUser? User { get; set; }

        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(65535)]
        [Column(TypeName = "TEXT")]
        public string Comment { get; set; } = string.Empty;

        [Range(1, 5)]
        public double Rating { get; set; } = 1;

        //Esse vai sevrir para de fato mostrar a data criado ou atualizado do review, o createdat do base não mudo
        public DateTime DateOfReview { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AeroclubeManager.Core.Entities
{
    public class BaseEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();  


        /// <summary>
        /// Com base no horário de Greenwich
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

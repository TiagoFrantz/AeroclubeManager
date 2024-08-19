using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AeroclubeManager.Core.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PerfilImage { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(15)]
        [Phone]
        public string PhoneNumber {  get; set; } = string.Empty;

        [Required]
        [MaxLength(15)]
        public string Document {  get; set; } = string.Empty; // CPF

        [Required]
        public DateTime DateOfBirth { get; set; } = DateTime.Today;

        [Required]
        public DateTime CreatedAt {  get; set; } = DateTime.Now;
    }
}

using System.ComponentModel.DataAnnotations;
using AeroclubeManager.Web.Validations;

namespace AeroclubeManager.Web.Models.Account
{
    public class ManagementAccountModelView
    {
        public string UserId { get; set; }

        [MaxLength(100, ErrorMessage = "O nome deve possuir até 100 caracteres.")]
        public string FirstName { get; set; } = string.Empty;


        [MaxLength(100, ErrorMessage = "O sobrenome deve possuir até 100 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } = DateTime.MinValue;

        public string Email { get; set; }

        public string PerfilImage { get; set; }


        [StringLength(15)]
        [Document(ErrorMessage = "CPF inválido.")] // Validação de CPF
        public string Document { get; set; } = string.Empty;
    }
}

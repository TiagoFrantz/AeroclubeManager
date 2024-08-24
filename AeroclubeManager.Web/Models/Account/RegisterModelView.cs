using System.ComponentModel.DataAnnotations;
using AeroclubeManager.Web.Validations;

namespace AeroclubeManager.Web.Models.Account
{
    /// <summary>
    /// Document é o CPF
    /// </summary>
    public class RegisterModelView
    {
        [Required]
        [MaxLength(100, ErrorMessage = "O nome deve possuir até 100 caracteres.")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "O sobrenome deve possuir até 100 caracteres.")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        [Document(ErrorMessage = "CPF inválido.")] // Validação de CPF
        public string Document { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password {  get; set; } = string.Empty;   

        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Senhas não estão iguais.")]
        public string ConfirmPassword { get; set;} = string.Empty;


        public string? ReturnUrl { get; set;} = string.Empty;
    }
}

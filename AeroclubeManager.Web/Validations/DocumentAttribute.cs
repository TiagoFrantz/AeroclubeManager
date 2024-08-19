using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AeroclubeManager.Web.Validations
{

    /// <summary>
    /// Valida CPF sem pontos ou outros caracteres.
    /// </summary>
    public class DocumentAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
                string cpf = value as string;
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
            cpf = Regex.Replace(cpf, @"\D", "");


            /*
                if (cpf.Length != 11)
                return new ValidationResult("CPF não possui 11 caracteres");*/

                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                
            resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                var valid = cpf.EndsWith(digito);

            if (!valid)
                return new ValidationResult("CPF inválido.");

                return ValidationResult.Success;
         }
    }
}

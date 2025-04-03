using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Bff.Validators
{
    [AttributeUsage(AttributeTargets.Property)]

    public class CnpjAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string cnpj || string.IsNullOrWhiteSpace(cnpj))
                return new ValidationResult(ErrorMessage ?? "CNPJ is required.");

            cnpj = Regex.Replace(cnpj, @"\D", ""); // Remove non-numeric characters

            if (cnpj.Length != 14 || !IsValidCnpj(cnpj))
                return new ValidationResult(ErrorMessage ?? "Invalid CNPJ.");

            return ValidationResult.Success;
        }

        private bool IsValidCnpj(string cnpj)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            int resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            string digito = resto.ToString();

            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Bff.Validators
{
    public class MinLengthAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public MinLengthAttribute(int minLength)
        {
            _minLength = minLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is ICollection<object> collection && collection.Count < _minLength)
            {
                return new ValidationResult(ErrorMessage ?? $"The collection must contain at least {_minLength} items.");
            }

            return ValidationResult.Success;
        }
    }
}

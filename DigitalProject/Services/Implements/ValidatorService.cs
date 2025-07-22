using DigitalProject.Services.Interface;
using System.ComponentModel.DataAnnotations;

namespace DigitalProject.Services.Implements
{
    public class ValidatorService: IValidatorService
    {
        public void Validate<T>(T model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, results, true);

            if (!isValid)
            {
                var messages = results.Select(r => r.ErrorMessage).ToList();
                var fullMessage = string.Join(" | ", messages);
                throw new ValidationException(fullMessage);
            }
        }
    }
}

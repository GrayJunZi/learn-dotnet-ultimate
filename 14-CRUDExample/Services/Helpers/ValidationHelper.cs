using System.ComponentModel.DataAnnotations;

namespace Services.Helpers;

public class ValidationHelper
{
    internal static void ModelValidation(object model)
    {
        ValidationContext validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);
        if (!isValid)
        {
            throw new ArgumentException(validationResults?.FirstOrDefault()?.ErrorMessage);
        }
    }
}
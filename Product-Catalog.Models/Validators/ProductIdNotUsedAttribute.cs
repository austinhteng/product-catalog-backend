using System.ComponentModel.DataAnnotations;

public sealed class ProductIdNotUsedAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(
        object value,
        ValidationContext validationContext)
    {
        //TODO: Check service for ID. Issue with circular dependency on ProductService.

        return ValidationResult.Success;
    }
}
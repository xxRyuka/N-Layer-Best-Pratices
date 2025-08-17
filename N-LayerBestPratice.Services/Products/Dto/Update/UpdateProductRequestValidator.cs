using FluentValidation;

namespace N_LayerBestPratice.Services.Products.Dto.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Product name cannot be null.")
            .NotEmpty().WithMessage("Product name is required.")
            .Length(3, 100).WithMessage("Product name must be between 3 and 100 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");
            

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.")
            .WithMessage("Stock quantity must be greater than or equal to zero.");

    }
}
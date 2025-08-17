using FluentValidation;

namespace N_LayerBestPratice.Services.Products.Dto.UpdateProductStock;

public class UpdateProductStockRequestValidator : AbstractValidator<UpdateProductStockRequest>
{
    public UpdateProductStockRequestValidator()
    {
        RuleFor(x=> x.id)
            .NotNull().WithMessage("Product ID cannot be null.")
            .GreaterThan(0).WithMessage("Product ID must be greater than zero.");
        
        RuleFor(x=> x.stockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.")
            .WithMessage("Stock quantity must be greater than or equal to zero.");
    }
}
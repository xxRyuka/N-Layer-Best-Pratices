using FluentValidation;

namespace N_LayerBestPratice.Services.Stores.Dto.Update;

public class UpdateStoreRequestValidator : AbstractValidator<UpdateStoreRequest>
{
    public UpdateStoreRequestValidator()
    {
        RuleFor(x=> x.StoreName)
            .NotNull().WithMessage("Store name cannot be null.")
            .NotEmpty().WithMessage("Store name is required.")
            .Length(3, 100).WithMessage("Store name must be between 3 and 100 characters.");
    }
}
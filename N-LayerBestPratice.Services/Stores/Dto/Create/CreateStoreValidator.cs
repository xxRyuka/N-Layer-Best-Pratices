using FluentValidation;

namespace N_LayerBestPratice.Services.Stores.Dto.Create;

public class CreateStoreValidator : AbstractValidator<CreateStoreRequest>
{
    public CreateStoreValidator()
    {
        RuleFor(x=> x.StoreName)
            .Length(3, 100).WithMessage("Store name must be between 3 and 100 characters.");
    }
}
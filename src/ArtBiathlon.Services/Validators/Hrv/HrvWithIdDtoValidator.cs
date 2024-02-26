using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.Hrv;

public class HrvWithIdDtoValidator : AbstractValidator<ModelDtoWithId<HrvDto>>
{
    public HrvWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Model)
            .SetValidator(new HrvDtoValidator());
    }
    
}
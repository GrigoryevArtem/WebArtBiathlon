using ArtBiathlon.Domain.Models.Ocr;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.Ocr;

public class OcrDtoValidator : AbstractValidator<OcrDto>
{
    public OcrDtoValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}
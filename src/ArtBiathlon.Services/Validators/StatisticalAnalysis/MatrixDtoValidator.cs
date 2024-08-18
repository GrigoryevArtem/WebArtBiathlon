using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.StatisticalAnalysis;

public class MatrixDtoValidator : AbstractValidator<MatrixDto>
{
    public MatrixDtoValidator()
    {
        RuleFor(x => x.Data.Length)
            .NotNull()
            .GreaterThan(0);
    }
}
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Api.Responses.V1.Ocr;

public record RecognizeImagesToTextResponse(HrvDto[] HrvIndicators);
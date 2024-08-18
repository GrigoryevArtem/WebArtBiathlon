using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.Ocr;

namespace ArtBiathlon.Domain.Interfaces.Services.Ocr;

public interface IOcrService
{
    Task<HrvDto[]> RecognizeImagesToText(
        OcrDto ocrDto,
        CancellationToken token);
}
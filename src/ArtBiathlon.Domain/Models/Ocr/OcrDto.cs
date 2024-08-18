using Microsoft.AspNetCore.Http;

namespace ArtBiathlon.Domain.Models.Ocr;

public record OcrDto(
    IEnumerable<IFormFile> Images,
    long UserId
);
using ArtBiathlon.Api.Responses.V1.Ocr;
using ArtBiathlon.Domain.Interfaces.Services.Ocr;
using ArtBiathlon.Domain.Models.Ocr;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/ocr")]
public class OcrController : ControllerBase
{
    private readonly IOcrService _ocrService;

    public OcrController(IOcrService ocrService)
    {
        _ocrService = ocrService;
    }

    [HttpPost("recognize-images-to-text")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<RecognizeImagesToTextResponse> RecognizeImagesToText(IFormFile[] images,
        CancellationToken token)
    {
        var userId = CurrentUser.GetId(HttpContext);
        var ocrDto = new OcrDto(images, userId);
        var hrvIndicators = await _ocrService.RecognizeImagesToText(ocrDto, token);
        return new RecognizeImagesToTextResponse(hrvIndicators);
    }
}
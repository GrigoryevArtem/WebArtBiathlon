using System.Globalization;
using System.Text.RegularExpressions;
using ArtBiathlon.Domain.Exceptions.Ocr;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Ocr;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.Ocr;
using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtBiathlon.Services.Services.Ocr;

internal class OcrService : IOcrService
{
    private const string HrvPattern =
        @"(\d+\,\d+|\d+)+%?(.*)eart rate (\d+)(.*)" +
        @"RMSSD (\d+)(.*)" +
        @"PNS index (-?\d+\,?\d+)(.*)" +
        @"SNS index (-?\d+\,?\d+)(.*)" +
        @"Mean RR (-?\d+.\d+)(.*)" +
        @"SDNN (-?\d+.\d+)(.*)" +
        @"Poincaré SD1 (-?\d+.\d+)(.*)" +
        @"Poincaré SD2 \D{0,2}(-?\d+.\d+)(.*)" +
        @"Stress index (-?\d+.\d+)(.*)" +
        @"Respiratory rate\D{0,2}(-?\d+.\d+)(.*)" +
        @"LF power (-?\d+.\d+)(.*)" +
        @"HF power (-?\d+.\d+)(.*)" +
        @"LF power \(n\,u\,\) (-?\d+.\d+)(.*)" +
        @"HF power \(n\,u\,\) (-?\d+.\d+)(.*)" +
        @"LF/HF ratio (-?\d+.\d+)(.*)";

    private const int FileNameLength = 2;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IHrvRepository _hrvRepository;
    private readonly IValidator<OcrDto> _validator;

    public OcrService(IHrvRepository hrvRepository, IWebHostEnvironment hostEnvironment,
        IConfiguration configuration, IValidator<OcrDto> validator)
    {
        _hrvRepository = hrvRepository;
        _hostEnvironment = hostEnvironment;
        _configuration = configuration;
        _validator = validator;
    }

    public async Task<HrvDto[]> RecognizeImagesToText(OcrDto ocrDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(ocrDto, token);

        var formFiles = ocrDto.Images as IFormFile[] ?? ocrDto.Images.ToArray();
        if (formFiles.IsNullOrEmpty()) throw new FilesNotFoundException();

        var hrvIndicators = new List<HrvDto>();

        foreach (var image in formFiles)
        {
            var date = ExtractDateFromFileName(image.FileName) ?? DateTimeOffset.UtcNow;
            var imagePath = await SaveImagePath(image);
            var text = ExtractTextFromImage(imagePath);
            var formattedText = FormatExtractedText(text);

            if (!Regex.IsMatch(formattedText, HrvPattern))
                continue;

            var matches = Regex.Matches(formattedText, HrvPattern);
            var hrv = CreateHrvDtoFromMatches(matches, date, ocrDto.UserId);
            hrvIndicators.Add(hrv);
            await _hrvRepository.CreateHrvAsync(hrv, token);
        }

        return hrvIndicators.ToArray();
    }

    private DateTimeOffset? ExtractDateFromFileName(string fileName)
    {
        var parts = fileName.Split(' ');
        if (parts.Length <= FileNameLength) return null;
        var dateStr = parts[FileNameLength];
        var lastDotIndex = dateStr.LastIndexOf('.');
        if (lastDotIndex != -1) dateStr = dateStr[..lastDotIndex];
        if (DateTimeOffset.TryParse(dateStr, null, DateTimeStyles.AssumeUniversal, out var date)) return date;
        return null;
    }

    private string ExtractTextFromImage(string imagePath)
    {
        var path = _configuration.GetValue<string>("TesseractTestData:Path");
        var language = _configuration.GetValue<string>("TesseractTestData:Language");
        using var tesseract = CreateTesseractLearn(path, language);
        tesseract.SetImage(new Image<Bgr, byte>(imagePath));
        tesseract.Recognize();
        return tesseract.GetUTF8Text();
    }

    private Tesseract CreateTesseractLearn(string? path, string? language)
    {
        if (path is null) throw new TesseractTestDataPathNotFoundException();
        if (language is null) throw new TesseractTestDataLanguageNotFoundException();
        return new Tesseract(path, language, OcrEngineMode.TesseractLstmCombined);
    }

    private string FormatExtractedText(string text)
    {
        return text.Replace("\n", "").Replace(".", ",");
    }

    private HrvDto CreateHrvDtoFromMatches(MatchCollection matches, DateTimeOffset date, long userId)
    {
        return new HrvDto(
            date.ToUniversalTime(),
            userId,
            double.Parse(matches[0].Groups[1].Value),
            int.Parse(matches[0].Groups[3].Value),
            int.Parse(matches[0].Groups[5].Value),
            double.Parse(matches[0].Groups[11].Value),
            double.Parse(matches[0].Groups[13].Value),
            double.Parse(matches[0].Groups[15].Value),
            double.Parse(matches[0].Groups[23].Value),
            double.Parse(matches[0].Groups[25].Value),
            double.Parse(matches[0].Groups[23].Value) + double.Parse(matches[0].Groups[25].Value),
            double.Parse(matches[0].Groups[19].Value),
            null
        );
    }

    private async Task<string> SaveImagePath(IFormFile image)
    {
        var imageName = Path.GetFileNameWithoutExtension(image.FileName);
        imageName = $"{imageName}{DateTime.Now:yyyy-MM-dd HH:mm:ss}{Path.GetExtension(image.FileName)}";
        var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
        await using var fileStream = new FileStream(imagePath, FileMode.Create);
        await image.CopyToAsync(fileStream);
        return imagePath;
    }
}
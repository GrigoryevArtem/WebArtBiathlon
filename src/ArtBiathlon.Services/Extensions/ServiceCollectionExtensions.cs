using System.Reflection;
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Ocr;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.ClusterAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.CorrelationAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.FactorAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.Training;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Services.Services.Hrv;
using ArtBiathlon.Services.Services.Hrv.HrvsFactory;
using ArtBiathlon.Services.Services.Ocr;
using ArtBiathlon.Services.Services.StatisticalAnalysis.ClusterAnalysis;
using ArtBiathlon.Services.Services.StatisticalAnalysis.CorrelationAnalysis;
using ArtBiathlon.Services.Services.StatisticalAnalysis.FactorAnalysis;
using ArtBiathlon.Services.Services.Training;
using ArtBiathlon.Services.Services.TrainingCamp;
using ArtBiathlon.Services.Services.TrainingsSchedule;
using ArtBiathlon.Services.Services.TrainingType;
using ArtBiathlon.Services.Services.User;
using ArtBiathlon.Services.Services.UserInfo;
using ArtBiathlon.Services.Validators.Hrv;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBiathlon.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services)
    {
        return services
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped<IOcrService, OcrService>()
            .AddScoped<IHrvService, HrvService>()
            .AddScoped<ITrainingService, TrainingService>()
            .AddScoped<ITrainingsScheduleService, TrainingsScheduleService>()
            .AddScoped<ITrainingCampService, TrainingCampService>()
            .AddScoped<ITrainingTypeService, TrainingTypeService>()
            .AddScoped<IUserCredentialService, UserCredentialService>()
            .AddScoped<ISignInService, SignInService>()
            .AddScoped<ISignUpService, SignUpService>()
            .AddScoped<IJwtService, JwtService>()
            .AddScoped<IUserInfoService, UserInfoService>()
            .AddSingleton<ICorrelationAnalysisService, CorrelationAnalysisService>()
            .AddSingleton<IFactorAnalysisService, FactorAnalysisService>()
            .AddSingleton<IClusterAnalysisService, ClusterAnalysisService>()
            .AddScoped<IValidator<HrvDto>, HrvDtoValidator>()
            .AddScoped<HrvsDayTypeFactory>();
    }
}
using ArtBiathlon.Domain.Interfaces.Services.Hrv;
using ArtBiathlon.Domain.Interfaces.Services.Ocr;
using ArtBiathlon.Domain.Interfaces.Services.Training;
using ArtBiathlon.Domain.Interfaces.Services.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Services.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Services.TrainingType;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models.User.UserInfo;
using ArtBiathlon.Services.Services.Hrv;
using ArtBiathlon.Services.Services.Ocr;
using ArtBiathlon.Services.Services.Training;
using ArtBiathlon.Services.Services.TrainingCamp;
using ArtBiathlon.Services.Services.TrainingsSchedule;
using ArtBiathlon.Services.Services.TrainingType;
using ArtBiathlon.Services.Services.User;
using ArtBiathlon.Services.Services.UserInfo;
using ArtBiathlon.Services.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBiathlon.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceCollection(
        this IServiceCollection services) 
    {
        services.AddScoped<IOcrService, OcrService>();
        services.AddScoped<IHrvService, HrvService>();
        services.AddScoped<ITrainingService, TrainingService>();
        services.AddScoped<ITrainingsScheduleService, TrainingsScheduleService>();
        services.AddScoped<ITrainingCampService, TrainingCampService>();
        services.AddScoped<ITrainingTypeService, TrainingTypeService>();
        services.AddScoped<IUserCredentialService, UserCredentialService>();
        services.AddScoped<ISignInService, SignInService>();
        services.AddScoped<ISignUpService, SignUpService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IUserInfoService, UserInfoService>();
        return services;
    }
}
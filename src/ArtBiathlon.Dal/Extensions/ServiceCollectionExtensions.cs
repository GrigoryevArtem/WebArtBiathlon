using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Dal.Infrastructure;
using ArtBiathlon.Dal.Repositories;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Interfaces.Dal.Training;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingCamp;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;
using ArtBiathlon.Domain.Interfaces.Dal.TrianingType;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBiathlon.Dal.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDalRepositories(
        this IServiceCollection services)
    {
      services.AddScoped<IHrvRepository, HrvRepository>();
      services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
      services.AddScoped<ITrainingCampRepository, TrainingCampRepository>();
      services.AddScoped<IUserInfoRepository, UserInfoRepository>();
      services.AddScoped<ITrainingsScheduleRepository, TrainingsScheduleRepository>();
      services.AddScoped<ITrainingRepository, TrainingRepository>();
      services.AddScoped<ITrainingTypeRepository, TrainingTypeRepository>();
      return services;
    }
    
    public static IServiceCollection AddDalInfrastructure(
        this IServiceCollection services, 
        IConfigurationRoot config)
    {
        //read config
        services.Configure<DalOptions>(config.GetSection(nameof(DalOptions)));

        //configure postrges types
        Postgres.MapCompositeTypes();
        
        //add migrations
        Postgres.AddMigrations(services);
        
        return services;
    }
}
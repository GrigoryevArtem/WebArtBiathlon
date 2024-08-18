using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Interfaces.Dal.Hrv;

public interface
    IHrvRepository
{
    Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>> GetHrvByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] userInfoIds, int[] campIds, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int userInfoIds, int trainingCampIds, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsWeekendAsync(int[] userInfoIds, int[] campIds, CancellationToken token);
    Task<ModelDtoWithId<HrvDto>[]> GetHrvsWeekdaysAsync(int[] userInfoIds, int[] campIds, CancellationToken token);
    Task DeleteHrvAsync(long id, CancellationToken token);
    Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token);
}
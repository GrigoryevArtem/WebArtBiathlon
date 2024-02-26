using ArtBiathlon.Domain.Models.Hrv;

namespace ArtBiathlon.Domain.Interfaces.Dal.Hrv;

public interface IHrvRepository
{
    Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token);
    Task<HrvDto> GetHrvByIdAsync(long id, CancellationToken token);
    Task<HrvDto[]> GetHrvsAsync(CancellationToken token);
    Task DeleteHrvAsync(long id, CancellationToken token);
    Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token);
}
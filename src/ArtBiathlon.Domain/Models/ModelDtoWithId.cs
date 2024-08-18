namespace ArtBiathlon.Domain.Models;

public record ModelDtoWithId<T>(
    long Id,
    T Model);
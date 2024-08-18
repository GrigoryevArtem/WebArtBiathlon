using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.User;

public record CurrentUserDto(
    string Login,
    string Surname,
    string Name,
    string MiddleName,
    DateTimeOffset BirthDate,
    Gender Gender,
    Rank Rank,
    Role Status,
    string Email,
    byte[] UserAvatar);
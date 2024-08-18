using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.User.UserInfo;

public record UserInfoDto(
    string Surname,
    string Name,
    string MiddleName,
    DateTimeOffset BirthDate,
    Gender Gender,
    Rank Rank,
    Role Status,
    string Email,
    byte[] UserAvatar
);
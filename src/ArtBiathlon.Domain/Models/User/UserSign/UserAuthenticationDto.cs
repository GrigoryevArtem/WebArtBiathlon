using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.User.UserSign;

public record UserAuthenticationDto(
    string UserName,
    byte[] PasswordHash,
    Role Role);
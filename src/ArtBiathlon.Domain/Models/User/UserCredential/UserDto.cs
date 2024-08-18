namespace ArtBiathlon.Domain.Models.User.UserCredential;

public record UserDto(
    string Login,
    byte[] PasswordHash);
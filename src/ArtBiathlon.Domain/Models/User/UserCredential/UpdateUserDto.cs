namespace ArtBiathlon.Domain.Models.User.UserCredential;

public record UpdateUserDto(
    string Login,
    string Password);
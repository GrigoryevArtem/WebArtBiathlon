using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Api.Requests.V1.User;

public record UpdateUserRequest(
    long Id,
    UpdateUserDto UpdateUserDto);
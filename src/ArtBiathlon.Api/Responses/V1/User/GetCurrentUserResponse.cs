using ArtBiathlon.Domain.Models.User;

namespace ArtBiathlon.Api.Responses.V1.User;

public record GetCurrentUserResponse(CurrentUserDto CurrentUserDto);
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Api.Responses.V1.User;

public record GetUserResponse(ModelDtoWithId<UserDto> UserModel);
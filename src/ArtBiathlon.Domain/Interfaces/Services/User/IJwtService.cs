using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserSign;

namespace ArtBiathlon.Domain.Interfaces.Services.User;

public interface IJwtService
{
    string GenerateToken(ModelDtoWithId<UserAuthenticationDto> userModelWithId);
}
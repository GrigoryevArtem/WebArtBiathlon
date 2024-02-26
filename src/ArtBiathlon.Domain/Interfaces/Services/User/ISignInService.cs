using ArtBiathlon.Domain.Models.User.UserSign;

namespace ArtBiathlon.Domain.Interfaces.Services.User;

public interface ISignInService
{
    Task<string> SignInAsync(SignInDto signInDto, CancellationToken token);
}
using ArtBiathlon.Domain.Models.User.UserSign;

namespace ArtBiathlon.Domain.Interfaces.Services.User;

public interface ISignUpService
{
    Task SignUpAsync(SignUpDto signUpDto, CancellationToken token);
    Task<string> SignUpAndSignInAsync(SignUpDto signUpDto, CancellationToken token);
}
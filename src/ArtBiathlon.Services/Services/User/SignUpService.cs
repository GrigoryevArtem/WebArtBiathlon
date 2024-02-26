using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models.User.UserCredential;
using ArtBiathlon.Domain.Models.User.UserSign;
using ArtBiathlon.Services.Helpers;

namespace ArtBiathlon.Services.Services.Users;

internal class SignUpService : ISignUpService
{
    private readonly ISignInService _signInService;
    private readonly IUserCredentialRepository _userRepository;

    public SignUpService(ISignInService signInService,
        IUserCredentialRepository userRepository)
    {
        _signInService = signInService;
        _userRepository = userRepository;
    }

    public async Task SignUpAsync(SignUpDto signUpDto, CancellationToken token)
    {
        //await _validator.ValidateAndThrowAsync(signUpModel);

        await AddUserAsync(signUpDto, token);
    }

    public async Task<string> SignUpAndSignInAsync(SignUpDto signUpDto, CancellationToken token)
    {
        await SignUpAsync(signUpDto, token);

        return await SignInAsync(signUpDto, token);
    }

    private async Task AddUserAsync(SignUpDto signUpDto, CancellationToken cancellationToken)
    {
        var userModel = ExtractUserModel(signUpDto);

        var user = new UserDto(userModel.UserName, userModel.PasswordHash);

        try
        {
            await _userRepository.CreateUserAsync(user, cancellationToken);
        }
        catch (UserNameAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
        catch (EmailAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    private async Task<string> SignInAsync(SignUpDto signUpDto, CancellationToken token)
    {
        var signInModel = ExtractSignInModel(signUpDto);

        return await _signInService.SignInAsync(signInModel, token);
    }

    private static UserAuthenticationDto ExtractUserModel(SignUpDto signUpDto)
    {
        return new UserAuthenticationDto(
            signUpDto.Login,
            HashPassword.GetPasswordHash(signUpDto.Password),
            Role.Biathlete);
    }

    private static SignInDto ExtractSignInModel(SignUpDto signUpDto)
    {
        return new SignInDto(
            signUpDto.Login,
            signUpDto.Password);
    }
}
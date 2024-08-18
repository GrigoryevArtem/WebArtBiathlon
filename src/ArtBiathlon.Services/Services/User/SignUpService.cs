using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models.User.UserCredential;
using ArtBiathlon.Domain.Models.User.UserSign;
using ArtBiathlon.Services.Helpers;
using FluentValidation;

namespace ArtBiathlon.Services.Services.User;

internal class SignUpService : ISignUpService
{
    private readonly ISignInService _signInService;
    private readonly IUserCredentialRepository _userRepository;
    private readonly IValidator<SignUpDto> _validator;

    public SignUpService(ISignInService signInService,
        IUserCredentialRepository userRepository,
        IValidator<SignUpDto> validator)
    {
        _signInService = signInService;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task SignUpAsync(SignUpDto signUpDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(signUpDto, token);
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
        await _userRepository.CreateUserAsync(user, cancellationToken);
    }

    private async Task<string> SignInAsync(SignUpDto signUpDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(signUpDto, token);
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
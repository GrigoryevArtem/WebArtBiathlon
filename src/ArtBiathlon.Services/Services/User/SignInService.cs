using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserSign;
using ArtBiathlon.Services.Helpers;
using FluentValidation;

namespace ArtBiathlon.Services.Services.User;

internal class SignInService : ISignInService
{
    private readonly IJwtService _jwtService;
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IValidator<SignInDto> _validator;

    public SignInService(
        IJwtService jwtService,
        IUserCredentialRepository userCredentialRepository,
        IUserInfoRepository userInfoRepository,
        IValidator<SignInDto> validator)
    {
        _jwtService = jwtService;
        _userCredentialRepository = userCredentialRepository;
        _userInfoRepository = userInfoRepository;
        _validator = validator;
    }

    public async Task<string> SignInAsync(SignInDto singInDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(singInDto, cancellationToken: token);

        var userModelWithId = await AuthenticateAndThrowAsync(singInDto, token);

        return await _jwtService.GenerateToken(userModelWithId, token);
    }

    private async Task<ModelDtoWithId<UserAuthenticationDto>> AuthenticateAndThrowAsync(SignInDto singInDto,
        CancellationToken token)
    {
        try
        {
            var userModel = await GetUserModelAsync(singInDto.Login, token);

            HashPassword.ThrowIfPasswordNotEqualsToHash(
                singInDto.Password,
                userModel.Model.PasswordHash);

            return userModel;
        }
        catch (IncorrectPasswordException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    private async Task<ModelDtoWithId<UserAuthenticationDto>> GetUserModelAsync(string login, CancellationToken token)
    {
        var user = await _userCredentialRepository.GetUserByLoginAsync(login, token);

        var userInfo = await _userInfoRepository.GetUserInfoByIdAsync(user.Id, token);

        return new ModelDtoWithId<UserAuthenticationDto>(
            user.Id,
            new UserAuthenticationDto(
                user.Model.Login,
                user.Model.PasswordHash,
                userInfo.Model.Status)
        );
    }
}
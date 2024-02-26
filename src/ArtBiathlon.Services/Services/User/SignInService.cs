using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserSign;
using ArtBiathlon.Services.Helpers;

namespace ArtBiathlon.Services.Services.Users;

internal class SignInService : ISignInService
{
    // private readonly IValidator<SignInModel> _validator;
    private readonly IJwtService _jwtService;
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IUserInfoRepository _userInfoRepository;

    public SignInService(
        //IValidator<SignInModel> validator,
        IJwtService jwtService,
        IUserCredentialRepository userCredentialRepository,
        IUserInfoRepository userInfoRepository)
    {
        //_validator = validator;
        _jwtService = jwtService;
        _userCredentialRepository = userCredentialRepository;
        _userInfoRepository = userInfoRepository;
    }

    public async Task<string> SignInAsync(SignInDto singInDto, CancellationToken token)
    {
        // await _validator.ValidateAndThrowAsync(signInModel);

        var userModelWithId = await AuthenticateAndThrowAsync(singInDto, token);

        return _jwtService.GenerateToken(userModelWithId);
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
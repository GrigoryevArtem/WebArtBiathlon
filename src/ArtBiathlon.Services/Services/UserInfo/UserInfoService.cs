using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;
using FluentValidation;

namespace ArtBiathlon.Services.Services.UserInfo;

internal class UserInfoService : IUserInfoService
{
    private readonly IUserInfoRepository _userInfoRepository;
    private readonly IValidator<UserInfoDto> _validator;

    public UserInfoService(IUserInfoRepository userInfoRepository, IValidator<UserInfoDto> validator)
    {
        _userInfoRepository = userInfoRepository;
        _validator = validator;
    }

    public async Task<long> CreateUserInfoAsync(UserInfoDto userInfoDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(userInfoDto, token);
        return await _userInfoRepository.CreateUserInfoAsync(userInfoDto, token);
    }

    public async Task<ModelDtoWithId<UserInfoDto>> GetUserInfoByIdAsync(long id, CancellationToken token)
    {
        return await _userInfoRepository.GetUserInfoByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<UserInfoDto>[]> GetUsersInfoAsync(CancellationToken token)
    {
        return await _userInfoRepository.GetUsersInfoAsync(token);
    }

    public async Task<ModelDtoWithId<UserInfoDto>[]> GetBiathletesAsync(CancellationToken token)
    {
        return await _userInfoRepository.GetBiathletesAsync(token);
    }

    public async Task<ModelDtoWithId<UserInfoDto>[]> GetTrainersAsync(CancellationToken token)
    {
        return await _userInfoRepository.GetTrainersAsync(token);
    }

    public async Task DeleteUserInfoAsync(long id, CancellationToken token)
    {
        await _userInfoRepository.DeleteUserInfoAsync(id, token);
    }

    public async Task UpdateUserInfoAsync(long id, UserInfoDto userInfoDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(userInfoDto, token);
        await _userInfoRepository.UpdateUserInfoAsync(id, userInfoDto, token);
    }
}
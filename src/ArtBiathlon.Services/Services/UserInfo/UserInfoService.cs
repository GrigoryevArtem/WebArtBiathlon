using System.Data;
using ArtBiathlon.Domain.Exceptions.UserInfo;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;

namespace ArtBiathlon.Services.Services.UserInfo;

internal class UserInfoService : IUserInfoService
{
    private readonly IUserInfoRepository _userInfoRepository;

    public UserInfoService(IUserInfoRepository userInfoRepository)
    {
        _userInfoRepository = userInfoRepository;
    }

    public async Task<long> CreateUserInfoAsync(UserInfoDto userInfoDto, CancellationToken token)
    {
        try
        {
            return await _userInfoRepository.CreateUserInfoAsync(userInfoDto, token);
        }
        catch (UserInfoAlreadyExistsException ex)
        {
            throw new DataException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<UserInfoDto>> GetUserInfoByIdAsync(long id, CancellationToken token)
    {
        try
        {
            return await _userInfoRepository.GetUserInfoByIdAsync(id, token);
        }
        catch (UserInfoNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<UserInfoDto>[]> GetUsersInfoAsync(CancellationToken token)
    {
        try
        {
            return await _userInfoRepository.GetUsersInfoAsync(token);
        }
        catch (UsersInfoNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }

    public async Task DeleteUserInfoAsync(long id, CancellationToken token)
    {
        try
        {
            await _userInfoRepository.DeleteUserInfoAsync(id, token);
        }
        catch (UserInfoNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }

    public async Task UpdateUserInfoAsync(long id, UserInfoDto userInfo, CancellationToken token)
    {
        try
        {
            await _userInfoRepository.UpdateUserInfoAsync(id, userInfo, token);
        }
        catch (UserInfoNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }
}
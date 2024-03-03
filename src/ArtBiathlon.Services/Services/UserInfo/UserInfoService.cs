using System.Data;
using ArtBiathlon.Domain.Exceptions.UserInfo;
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
        try
        {
            await _validator.ValidateAndThrowAsync(userInfoDto, cancellationToken: token);
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

    public async Task UpdateUserInfoAsync(long id, UserInfoDto userInfoDto, CancellationToken token)
    {
        try
        {
            await _validator.ValidateAndThrowAsync(userInfoDto, cancellationToken: token);
            await _userInfoRepository.UpdateUserInfoAsync(id, userInfoDto, token);
        }
        catch (UserInfoNotFoundException ex)
        {
            throw new DataException(ex.Message);
        }
    }
}
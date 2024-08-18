using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;

namespace ArtBiathlon.Domain.Interfaces.Dal.UserInfo;

public interface IUserInfoRepository
{
    Task<long> CreateUserInfoAsync(UserInfoDto userInfoDto, CancellationToken token);
    Task<ModelDtoWithId<UserInfoDto>> GetUserInfoByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<UserInfoDto>[]> GetUsersInfoAsync(CancellationToken token);
    Task<ModelDtoWithId<UserInfoDto>[]> GetBiathletesAsync(CancellationToken token);
    Task<ModelDtoWithId<UserInfoDto>[]> GetTrainersAsync(CancellationToken token);
    Task DeleteUserInfoAsync(long id, CancellationToken token);
    Task UpdateUserInfoAsync(long id, UserInfoDto userInfo, CancellationToken token);
}
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Domain.Interfaces.Dal.User;

public interface IUserCredentialRepository
{
    Task<long> CreateUserAsync(UserDto userDto, CancellationToken token);
    Task<ModelDtoWithId<UserDto>> GetUserByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<UserDto>[]> GetUsersAsync(CancellationToken token);
    Task DeleteUserAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<UserDto>> GetUserByLoginAsync(string login, CancellationToken token);
    Task UpdateUserAsync(long id, UserDto user, CancellationToken token);
}
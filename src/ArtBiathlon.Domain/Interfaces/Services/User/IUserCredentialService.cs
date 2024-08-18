using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Domain.Interfaces.Services.User;

public interface IUserCredentialService
{
    Task<long> CreateUserAsync(UserDto userDto, CancellationToken token);
    Task<ModelDtoWithId<UserDto>> GetUserByIdAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<UserDto>[]> GetUsersAsync(CancellationToken token);
    Task DeleteUserAsync(long id, CancellationToken token);
    Task<ModelDtoWithId<UserDto>> GetUserByLoginAsync(string login, CancellationToken token);
    Task UpdateUserAsync(long id, UpdateUserDto user, CancellationToken token);
}
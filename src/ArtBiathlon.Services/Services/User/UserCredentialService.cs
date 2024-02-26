using ArtBiathlon.Domain.Exceptions.Infrastructure;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Services.Services.User;

public class UserCredentialService : IUserCredentialService
{
    private readonly IUserCredentialRepository _userCredentialRepository;

    public UserCredentialService(IUserCredentialRepository userCredentialRepository)
    {
        _userCredentialRepository = userCredentialRepository;
    }

    public async Task<long> CreateUserAsync(UserDto userDto, CancellationToken token)
    {
        try
        {
            return await _userCredentialRepository.CreateUserAsync(userDto, token);
        }
        catch (UserNameAlreadyExistsException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<UserDto>> GetUserByIdAsync(long id, CancellationToken token)
    {
        try
        {
            return await _userCredentialRepository.GetUserByIdAsync(id, token);
        }
        catch (UserNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<UserDto>[]> GetUsersAsync(CancellationToken token)
    {
        try
        {
            return await _userCredentialRepository.GetUsersAsync(token);
        }
        catch (UserNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task DeleteUserAsync(long id, CancellationToken token)
    {
        try
        {
            await _userCredentialRepository.DeleteUserAsync(id, token);
        }
        catch (UserNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task<ModelDtoWithId<UserDto>> GetUserByLoginAsync(string login, CancellationToken token)
    {
        try
        {
            return await _userCredentialRepository.GetUserByLoginAsync(login, token);
        }
        catch (UserNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }

    public async Task UpdateUserAsync(long id, UserDto user, CancellationToken token)
    {
        try
        {
            await _userCredentialRepository.UpdateUserAsync(id, user, token);
        }
        catch (UserNotFoundException ex)
        {
            throw new DomainException(ex.Message);
        }
    }
}
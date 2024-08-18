using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;
using ArtBiathlon.Services.Helpers;
using FluentValidation;

namespace ArtBiathlon.Services.Services.User;

public class UserCredentialService : IUserCredentialService
{
    private readonly IUserCredentialRepository _userCredentialRepository;
    private readonly IValidator<UserDto> _validator;
    private readonly IValidator<UpdateUserDto> _validatorUpdate;

    public UserCredentialService(IUserCredentialRepository userCredentialRepository, IValidator<UserDto> validator,
        IValidator<UpdateUserDto> validatorUpdate)
    {
        _userCredentialRepository = userCredentialRepository;
        _validator = validator;
        _validatorUpdate = validatorUpdate;
    }

    public async Task<long> CreateUserAsync(UserDto userDto, CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(userDto, token);
        return await _userCredentialRepository.CreateUserAsync(userDto, token);
    }

    public async Task<ModelDtoWithId<UserDto>> GetUserByIdAsync(long id, CancellationToken token)
    {
        return await _userCredentialRepository.GetUserByIdAsync(id, token);
    }

    public async Task<ModelDtoWithId<UserDto>[]> GetUsersAsync(CancellationToken token)
    {
        return await _userCredentialRepository.GetUsersAsync(token);
    }

    public async Task DeleteUserAsync(long id, CancellationToken token)
    {
        await _userCredentialRepository.DeleteUserAsync(id, token);
    }

    public async Task<ModelDtoWithId<UserDto>> GetUserByLoginAsync(string login, CancellationToken token)
    {
        return await _userCredentialRepository.GetUserByLoginAsync(login, token);
    }

    public async Task UpdateUserAsync(long id, UpdateUserDto userUpdateDto, CancellationToken token)
    {
        await _validatorUpdate.ValidateAndThrowAsync(userUpdateDto, token);
        var updatedUser = new UserDto(userUpdateDto.Login, HashPassword.GetPasswordHash(userUpdateDto.Password));
        await _userCredentialRepository.UpdateUserAsync(id, updatedUser, token);
    }
}
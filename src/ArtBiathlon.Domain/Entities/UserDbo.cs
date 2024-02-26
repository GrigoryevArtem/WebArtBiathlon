using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Domain.Entities;

public record UserDbo
{
    public long Id { get; init; }
    public string Login { get; init; }
    public byte[] Password { get; init; }

    public UserDbo()
    {
    }
    
    public UserDbo(long id, string login, byte[] password)
    {
        Id = id;
        Login = login;
        Password = password;
    }

    public ModelDtoWithId<UserDto> ToModelWithId()
    {
        var userModel = new UserDto(
            Login,
            Password);

        return new ModelDtoWithId<UserDto>(
            Id,
            userModel);
    }
}
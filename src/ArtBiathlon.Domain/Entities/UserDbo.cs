using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;

namespace ArtBiathlon.Domain.Entities;

public record UserDbo
{
    public UserDbo()
    {
    }

    public UserDbo(long userInfoId, string login, byte[] password)
    {
        UserInfoId = userInfoId;
        Login = login;
        Password = password;
    }

    public long UserInfoId { get; init; }
    public string Login { get; init; }
    public byte[] Password { get; init; }

    public ModelDtoWithId<UserDto> ToModelWithId()
    {
        var userModel = new UserDto(
            Login,
            Password);

        return new ModelDtoWithId<UserDto>(
            UserInfoId,
            userModel);
    }
}
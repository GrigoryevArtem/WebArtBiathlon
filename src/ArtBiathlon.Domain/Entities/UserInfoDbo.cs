using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;

namespace ArtBiathlon.Domain.Entities;

public record UserInfoDbo
{
    public UserInfoDbo()
    {
    }

    public UserInfoDbo(
        long id,
        string surname,
        string name,
        string middleName,
        DateTimeOffset birthDate,
        Gender gender,
        Rank rank,
        Role status,
        string email,
        byte[] userAvatar)
    {
        Id = id;
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        BirthDate = birthDate;
        Gender = gender;
        Rank = rank;
        Status = status;
        Email = email;
        UserAvatar = userAvatar;
    }

    public long Id { get; init; }
    public string Surname { get; init; }
    public string Name { get; init; }
    public string MiddleName { get; init; }
    public DateTimeOffset BirthDate { get; init; }
    public Gender Gender { get; init; }
    public Rank Rank { get; init; }
    public Role Status { get; init; }
    public string Email { get; init; }
    public byte[] UserAvatar { get; init; }

    public ModelDtoWithId<UserInfoDto> ToModelWithId()
    {
        var userInfoModel = new UserInfoDto(
            Surname,
            Name,
            MiddleName,
            BirthDate,
            Gender,
            Rank,
            Status,
            Email,
            UserAvatar
        );

        return new ModelDtoWithId<UserInfoDto>(
            Id,
            userInfoModel);
    }
}
using ArtBiathlon.Domain.Models.User.UserInfo;

namespace ArtBiathlon.Api.Requests.V1.UserInfo;

public record UpdateUserInfoRequest(
    long Id,
    UserInfoDto UserInfoDto);
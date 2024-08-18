using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;

namespace ArtBiathlon.Api.Responses.V1.UserInfo;

public record GetUserInfoResponse(ModelDtoWithId<UserInfoDto> UserInfoModel);
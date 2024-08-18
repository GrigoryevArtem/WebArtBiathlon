using ArtBiathlon.Api.Requests.V1.UserInfo;
using ArtBiathlon.Api.Responses.V1.UserInfo;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/user-information")]
public class UserInfoController : ControllerBase
{
    private readonly IUserInfoService _userInfoService;

    public UserInfoController(IUserInfoService userInfoService)
    {
        _userInfoService = userInfoService;
    }

    [HttpGet("get-user-information-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserInfoResponse> GeById(long id, CancellationToken token)
    {
        var userIndoModel = await _userInfoService.GetUserInfoByIdAsync(id, token);
        return new GetUserInfoResponse(userIndoModel);
    }

    [HttpPost("create-user-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(AddUserInfoRequest request, CancellationToken token)
    {
        await _userInfoService.CreateUserInfoAsync(request.UserInfoDto, token);
        return Ok();
    }

    [HttpGet("get-users-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUsersInfoResponse> Get(CancellationToken token)
    {
        var userInfoModels = await _userInfoService.GetUsersInfoAsync(token);
        return new GetUsersInfoResponse(userInfoModels);
    }

    [HttpGet("get-biathletes-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUsersInfoResponse> GetBiathletes(CancellationToken token)
    {
        var userInfoModels = await _userInfoService.GetBiathletesAsync(token);
        return new GetUsersInfoResponse(userInfoModels);
    }

    [HttpGet("get-trainers-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUsersInfoResponse> GetTrainers(CancellationToken token)
    {
        var userInfoModels = await _userInfoService.GetTrainersAsync(token);
        return new GetUsersInfoResponse(userInfoModels);
    }

    [HttpDelete("delete-user-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(long id, CancellationToken token)
    {
        await _userInfoService.DeleteUserInfoAsync(id, token);
    }

    [HttpPut("update-user-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update(UpdateUserInfoRequest request, CancellationToken token)
    {
        await _userInfoService.UpdateUserInfoAsync(request.Id, request.UserInfoDto, token);
    }
}
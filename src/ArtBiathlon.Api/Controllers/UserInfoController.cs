using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;
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
    public async Task<ModelDtoWithId<UserInfoDto>> GeById(long id, CancellationToken token)
    {
        return await _userInfoService.GetUserInfoByIdAsync(id, token);
    }
    
    [HttpPost("create-user-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(UserInfoDto userInfoDto, CancellationToken token)
    { 
        await _userInfoService.CreateUserInfoAsync(userInfoDto, token);
        return Ok();
    }
    
    [HttpGet("get-users-information")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<UserInfoDto>[]> Get(CancellationToken token)
    {
        return await _userInfoService.GetUsersInfoAsync(token);
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
    public async Task Update(long id, UserInfoDto userInfoDto, CancellationToken token)
    {
        await _userInfoService.UpdateUserInfoAsync(id, userInfoDto, token);
    }
}
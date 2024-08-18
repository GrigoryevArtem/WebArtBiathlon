using System.Security.Claims;
using ArtBiathlon.Api.Requests.V1.User;
using ArtBiathlon.Api.Responses.V1.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Interfaces.Services.UserInfo;
using ArtBiathlon.Domain.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/user")]
public class UserController : ControllerBase
{
    private readonly CookieOptions _jwtCookieOptions = new()
    {
        HttpOnly = true,
        Expires = DateTimeOffset.Now.AddDays(7)
    };

    private readonly ISignInService _signInService;
    private readonly ISignUpService _signUpService;
    private readonly IUserCredentialService _userCredentialService;
    private readonly IUserInfoService _userInfoService;

    public UserController(
        IUserCredentialService userCredentialService,
        ISignInService signInService,
        ISignUpService signUpService,
        IUserInfoService userInfoService)
    {
        _userCredentialService = userCredentialService;
        _signInService = signInService;
        _signUpService = signUpService;
        _userInfoService = userInfoService;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignIn(SignInRequest request, CancellationToken token)
    {
        var jwtToken = await _signInService.SignInAsync(request.SignInDto, token);
        Response.Cookies.Append("jwt", jwtToken, _jwtCookieOptions);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignUp(SignUpRequest request, CancellationToken token)
    {
        await _signUpService.SignUpAsync(request.SignUpDto, token);
    }

    [HttpPost("sign-up-and-sign-in")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignUpAndSignIn(SignUpRequest request, CancellationToken token)
    {
        var jwtToken = await _signUpService.SignUpAndSignInAsync(request.SignUpDto, token);
        Response.Cookies.Append("jwt", jwtToken, _jwtCookieOptions);
    }

    [HttpPost("sign-out")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public new IActionResult SignOut()
    {
        Response.Cookies.Delete("jwt");
        return Ok();
    }

    [HttpGet("get-current-user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetCurrentUserResponse> GetCurrentUser(CancellationToken token)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var userId = long.Parse(identity!.FindFirst("UserId")!.Value);
        var userCredential = await _userCredentialService.GetUserByIdAsync(userId, token);
        var userInfo = await _userInfoService.GetUserInfoByIdAsync(userId, token);
        var userInfoModel = userInfo.Model;

        var currentUser = new CurrentUserDto(
            userCredential.Model.Login,
            userInfoModel.Surname,
            userInfoModel.Name,
            userInfoModel.MiddleName,
            userInfoModel.BirthDate,
            userInfoModel.Gender,
            userInfoModel.Rank,
            userInfoModel.Status,
            userInfoModel.Email,
            userInfoModel.UserAvatar);

        return new GetCurrentUserResponse(currentUser);
    }

    [HttpGet("get-user-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserResponse> GeById(long id, CancellationToken token)
    {
        var user = await _userCredentialService.GetUserByIdAsync(id, token);
        return new GetUserResponse(user);
    }

    [HttpGet("get-user-by-login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUserResponse> GeByLogin(string login, CancellationToken token)
    {
        var user = await _userCredentialService.GetUserByLoginAsync(login, token);
        return new GetUserResponse(user);
    }

    [HttpGet("get-users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetUsersResponse> Get(CancellationToken token)
    {
        var users = await _userCredentialService.GetUsersAsync(token);
        return new GetUsersResponse(users);
    }

    [HttpDelete("delete-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Delete(long id, CancellationToken token)
    {
        await _userCredentialService.DeleteUserAsync(id, token);
    }

    [HttpPut("update-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task Update(UpdateUserRequest request, CancellationToken token)
    {
        await _userCredentialService.UpdateUserAsync(request.Id, request.UpdateUserDto, token);
    }
}
using System.Security.Claims;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;
using ArtBiathlon.Domain.Models.User.UserSign;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtBiathlon.Api.Controllers;

[ApiController]
[Route("/v1/user")]
public class UserController : ControllerBase
{
    private readonly IUserCredentialService _userCredentialService;
    private readonly ISignInService _signInService;
    private readonly ISignUpService _signUpService;

    private readonly CookieOptions _jwtCookieOptions = new()
    {
        HttpOnly = true,
        Expires = DateTimeOffset.Now.AddDays(7),
    };

    public UserController(IUserCredentialService userCredentialService, ISignInService signInService,
        ISignUpService signUpService)
    {
        _userCredentialService = userCredentialService;
        _signInService = signInService;
        _signUpService = signUpService;
    }

    [HttpPost("sign-in")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignIn(SignInDto signInDto, CancellationToken token)
    {
        var jwtToken = await _signInService.SignInAsync(signInDto, token);

        Response.Cookies.Append("jwt", jwtToken, _jwtCookieOptions);
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignUp(SignUpDto signUpDto, CancellationToken token)
    {
        await _signUpService.SignUpAsync(signUpDto, token);
    }

    [HttpPost("sign-up-and-sign-in")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task SignUpAndSignIn(SignUpDto signUpDto, CancellationToken token)
    {
        var jwtToken = await _signUpService.SignUpAndSignInAsync(signUpDto, token);
        
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
    
   /* [HttpGet("get-user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetCurrentUserResponse> GetUsername()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        var userId = long.Parse(identity!.FindFirst("UserId")!.Value); // todo: const?
        
        var getUserQuery = new GetUserByIdQuery(userId);

        var response = await _mediator.Send(getUserQuery);

        return new GetCurrentUserResponse(
            response.UserModelWithId.Id,
            response.UserModelWithId.UserModel.UserName,
            response.UserModelWithId.UserModel.Email);
    }*/
    
    [HttpGet("get-user-by-id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<UserDto>> GeById(long id, CancellationToken token)
    {
        return await _userCredentialService.GetUserByIdAsync(id, token);
    }

    [HttpGet("get-user-by-login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<UserDto>> GeByLogin(string login, CancellationToken token)
    {
        return await _userCredentialService.GetUserByLoginAsync(login, token);
    }

    [HttpPost("create-user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Create(UserDto userDto, CancellationToken token)
    {
        await _userCredentialService.CreateUserAsync(userDto, token);
        return Ok();
    }

    [HttpGet("get-users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ModelDtoWithId<UserDto>[]> Get(CancellationToken token)
    {
        return await _userCredentialService.GetUsersAsync(token);
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
    public async Task Update(long id, UserDto userDto, CancellationToken token)
    {
        await _userCredentialService.UpdateUserAsync(id, userDto, token);
    }
}
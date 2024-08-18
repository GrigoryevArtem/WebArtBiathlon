using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserSign;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ArtBiathlon.Services.Services.User;

internal class JwtService : IJwtService
{
    private readonly string _audience;
    private readonly SigningCredentials _credentials;
    private readonly string _issuer;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    private readonly IValidator<ModelDtoWithId<UserAuthenticationDto>> _validator;

    public JwtService(IConfiguration configuration, IValidator<ModelDtoWithId<UserAuthenticationDto>> validator)
    {
        var jwtKey = configuration.GetSection("Jwt:Key").Value!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        _validator = validator;
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        _issuer = configuration.GetSection("Jwt:Issuer").Value!;
        _audience = configuration.GetSection("Jwt:Audience").Value!;
    }

    public async Task<string> GenerateToken(ModelDtoWithId<UserAuthenticationDto> userModelWithId,
        CancellationToken token)
    {
        await _validator.ValidateAndThrowAsync(userModelWithId, token);
        var claims = ExtractClaims(userModelWithId);
        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: _credentials);

        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    private static Claim[] ExtractClaims(ModelDtoWithId<UserAuthenticationDto> userModelWithId)
    {
        return new[]
        {
            new Claim("UserId", userModelWithId.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userModelWithId.Model.UserName),
            new Claim(ClaimTypes.Role, ((int)userModelWithId.Model.Role).ToString())
        };
    }
}
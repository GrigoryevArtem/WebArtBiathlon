using System.Text;
using ArtBiathlon.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ArtBiathlon.Domain.Interfaces.Services.User;
using ArtBiathlon.Domain.Models.User.UserSign;
using Microsoft.IdentityModel.Tokens;

namespace ArtBiathlon.Services.Services.Users;

internal class JwtService : IJwtService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly SigningCredentials _credentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public JwtService(IConfiguration configuration)
    {
        var jwtKey = configuration.GetSection("Jwt:Key").Value!;
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        _issuer = configuration.GetSection("Jwt:Issuer").Value!;
        _audience = configuration.GetSection("Jwt:Audience").Value!;
    }

    public string GenerateToken(ModelDtoWithId<UserAuthenticationDto> userModelWithId)
    {
        var claims = ExtractClaims(userModelWithId);
        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddDays(7), //todo: const; set 30 minutes when will add refresh token
            signingCredentials: _credentials);

        return _jwtSecurityTokenHandler.WriteToken(token);
    }

    private static IEnumerable<Claim> ExtractClaims(ModelDtoWithId<UserAuthenticationDto> userModelWithId)
    {
        return new[]
        {
            new Claim("UserId", userModelWithId.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, userModelWithId.Model.UserName),
            new Claim(ClaimTypes.Role, ((int)userModelWithId.Model.Role).ToString())
        };
    }
}
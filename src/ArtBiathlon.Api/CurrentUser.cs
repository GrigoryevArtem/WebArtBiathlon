using System.Security.Claims;
using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Api;

public static class CurrentUser
{
    public static long GetId(HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;

        var userId = long.Parse(identity!.FindFirst("UserId")!.Value);

        return userId;
    }

    public static Role GetRole(HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        var userRole = long.Parse(identity!.FindFirst(ClaimTypes.Role)!.Value);
        return (Role)userRole;
    }
}
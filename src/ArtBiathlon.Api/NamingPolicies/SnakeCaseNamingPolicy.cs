using System.Text.Json;
using ArtBiathlon.Api.Extensions;

namespace ArtBiathlon.Api.NamingPolicies;

public class SnakeCaseNamingPolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToSnakeCase();
    }
}
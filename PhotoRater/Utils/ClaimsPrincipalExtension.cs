using System.Security.Claims;

namespace PhotoRater.Utils;

public static class ClaimsPrincipalExtension
{
    public static string? GetUserId(this ClaimsPrincipal cp)
    {
        return cp.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
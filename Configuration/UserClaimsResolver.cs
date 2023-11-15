using ReactAppBackend.Models;
using System.Security.Claims;

namespace ReactAppBackend.Configuration;

public class UserClaimsResolver : IUser
{
    public UserClaimsResolver(IHttpContextAccessor httpContextAccessor)
    {
        var identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        if (identity == null)
        {
            throw new Exception(nameof(identity));
        }

        Username = identity.FindFirst("username")?.Value!;
        UserId = int.Parse(identity.FindFirst("userId")?.Value!);
        UserRole = (UserRoleEnum)int.Parse(identity.FindFirst("userRole")?.Value!);
    }

    public int UserId { get; }

    public string Username { get; }

    public UserRoleEnum UserRole { get; }
}

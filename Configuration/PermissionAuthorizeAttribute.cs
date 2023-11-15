using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReactAppBackend.Configuration;
using ReactAppBackend.Models;

namespace ReactAppBackend.Configuration;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly UserRoleEnum _permissionType;

    public PermissionAuthorizeAttribute(UserRoleEnum permissionType)
    {
        _permissionType = permissionType;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.RequestServices.GetRequiredService<IUser>();
        if (!user.UserRole.Equals(_permissionType))
        {
            context.Result = new ForbidResult();
        }
    }
}

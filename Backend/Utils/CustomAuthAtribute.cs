using Dtos.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Utils;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(UserType roleEnum)
    {
        Roles = roleEnum.ToString().Replace(" ", string.Empty);
    }   
}

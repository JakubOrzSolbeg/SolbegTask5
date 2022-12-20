using DataRepository.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;

namespace Backend.Utils;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(UserType roleEnum)
    {
        Roles = roleEnum.ToString().Replace(" ", string.Empty);
    }   
}

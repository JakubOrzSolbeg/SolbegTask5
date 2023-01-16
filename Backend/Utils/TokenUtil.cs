using Services.Interfaces;

namespace Backend.Utils;

public class TokenUtil
{
    private readonly ITokenService _tokenService;

    public TokenUtil(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public int GetUserId(HttpContext context)
    {
        var tokenFromRequest = context.Request.Headers["Authorization"];
        if (tokenFromRequest.Count > 0)
        {
            var token = tokenFromRequest[0].Split(" ")[1];
            return _tokenService.GetUserId(token);
        }
        else
        {
            return -1;
        }
    }
    
}
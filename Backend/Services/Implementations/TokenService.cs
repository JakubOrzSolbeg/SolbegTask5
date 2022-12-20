using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Backend.Services.Interfaces;
using DataRepository.Entities;
using DataRepository.Enums;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _jwtkey;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    
    public readonly TokenValidationParameters ValidationParameters;
    public TokenService(IConfiguration configuration)
    {
        _jwtkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["jwtkey"]));
        ValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _jwtkey,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        _tokenHandler = new JwtSecurityTokenHandler();
    }
    public bool IsTokenValid(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }

        try
        {
            _tokenHandler.ValidateToken(token, ValidationParameters, out SecurityToken validatedToken);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public int GetUserId(string token)
    {
        var jwtSecurityToken = new JwtSecurityToken(token);
        return GetUserId(jwtSecurityToken);
    }

    public int GetUserId(HttpContext context)
    {
        var tokenFromRequest = context.Request.Headers["Authorization"];
        if (tokenFromRequest.Count > 0)
        {
            var token = tokenFromRequest[0].Split(" ")[1];
            return GetUserId(token);
        }
        else
        {
            return -1;
        }
    }
    
    public int GetUserId(JwtSecurityToken token)
    {
        var userId = token.Claims.FirstOrDefault(claim => claim.Type == "userId")?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            throw new ArgumentNullException(nameof(token));
        }

        return int.Parse(userId);
    }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
            new Claim("userName", user.Login),
            new Claim("role", user.UserType.ToString())
        };

        var signingCred = new SigningCredentials(_jwtkey, SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
            issuer: "localhost",
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: signingCred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

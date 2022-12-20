using DataRepository.Entities;

namespace Backend.Services.Interfaces;

public interface ITokenService
{
    //Yoinked from my diploma code
    public bool IsTokenValid(string token);
    public int GetUserId(string token);
    public int GetUserId(HttpContext context);
    public string GenerateToken(User user);
}

using DataRepository.Entities;

namespace Services.Interfaces;

public interface ITokenService
{
    //Yoinked from my diploma code
    public bool IsTokenValid(string token);
    public int GetUserId(string token);
    public string GenerateToken(User user);
}

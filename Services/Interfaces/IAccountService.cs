using Dtos.Requests;
using Dtos.Results;

namespace Services.Interfaces;

public interface IAccountService
{
    public Task<ApiResultBase<string>> RegisterUser(LoginCredentials registerCredentials);
    public Task<ApiResultBase<string>> LoginUser(LoginCredentials loginCredentials);
    public Task<ApiResultBase<bool>> PromoteUser(string userLogin);
    public Task<ApiResultBase<bool>> DemoteUser(string userLogin);
    public Task<ApiResultBase<AccountDetails>> GetUserInfo(int userId);
}
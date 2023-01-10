using Backend.Services.Interfaces;
using Backend.Utils;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Dtos.Enums;
using Dtos.Requests;
using Dtos.Results;

namespace Backend.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly ExtendedRepository<User> _accountsRepository;
    private readonly ITokenService _tokenService;
    public AccountService(ExtendedRepository<User> accountRepository, ITokenService tokenService)
    {
        _accountsRepository = accountRepository;
        _tokenService = tokenService;
    }
    
    public async Task<ApiResultBase<string>> RegisterUser(LoginCredentials registerCredentials)
    {
        var existingUser = await _accountsRepository.GetByPredicate(user => user.Login.Equals(registerCredentials.Login));
        if (existingUser != null)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = $"Login {registerCredentials.Login} is already taken"
            };
        }
        
        string salt = RandomStringGenerator.GenerateRandomString(1);
        string passhash = Sha256HashGenerator.ComputeSha256Hash(registerCredentials.Password + salt);
        var newUser = new User()
        {
            Login = registerCredentials.Login,
            Passhash = passhash,
            Salt = salt,
            UserType = UserType.User
        };
        await _accountsRepository.Add(newUser);
        string token = _tokenService.GenerateToken(newUser);
        return new ApiResultBase<string>()
        {
            IsSuccess = true,
            Body = token
        };
    }

    public async Task<ApiResultBase<string>> LoginUser(LoginCredentials loginCredentials)
    {
        var existingUser = await _accountsRepository.GetByPredicate(user => user.Login.Equals(loginCredentials.Login));
        if (existingUser == null)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = "Wrong login or password"
            };
        }

        var passhash = Sha256HashGenerator.ComputeSha256Hash(loginCredentials.Password + existingUser.Salt);
        if (passhash != existingUser.Passhash)
        {
            return new ApiResultBase<string>()
            {
                IsSuccess = false,
                Errors = "Wrong login or password"
            };
        }

        string token = _tokenService.GenerateToken(existingUser);
        return new ApiResultBase<string>()
        {
            IsSuccess = true,
            Body = token
        };
    }

    public async Task<ApiResultBase<bool>> PromoteUser(string userLogin)
    {
        var userForPromotion = await _accountsRepository.GetByPredicate(user => user.Login.Equals(userLogin));
        if (userForPromotion == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "User does not exists"
            };
        }

        userForPromotion.UserType = UserType.Worker;
        await _accountsRepository.Update(userForPromotion);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<bool>> DemoteUser(string userLogin)
    {
        var userForDemotion = await _accountsRepository.GetByPredicate(user => user.Login.Equals(userLogin));
        if (userForDemotion == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "User does not exists"
            };
        }

        userForDemotion.UserType = UserType.User;
        await _accountsRepository.Update(userForDemotion);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<AccountDetails>> GetUserInfo(int userId)
    {
        var user = await _accountsRepository.GetById(userId);
        if (user == null)
        {
            return new ApiResultBase<AccountDetails>()
            {
                IsSuccess = false,
                Errors = "User does not exists"
            };
        }

        var result = new AccountDetails()
        {
            Login = user.Login,
            Permissions = user.UserType.ToString()
        };

        return new ApiResultBase<AccountDetails>()
        {
            IsSuccess = true,
            Body = result
        };
    }
}
using Backend.Utils;
using Dtos.Enums;
using Dtos.Requests;
using Dtos.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AccountsController : ControllerBase
{

    private readonly IAccountService _accountService;
    private readonly TokenUtil _tokenService;
    public AccountsController(IAccountService accountService, TokenUtil tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }
    [HttpPost]
    public async Task<ActionResult<ApiResultBase<string>>> Register(LoginCredentials registerCredentials)
    {
        var registerResult = await _accountService.RegisterUser(registerCredentials);
        return registerResult;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResultBase<string>>> Login(LoginCredentials loginCredentials)
    {
        var loginResult = await _accountService.LoginUser(loginCredentials);
        return loginResult;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResultBase<AccountDetails>>> MyAccount()
    {
        int userId = _tokenService.GetUserId(HttpContext);
        return await _accountService.GetUserInfo(userId);
    }

    [HttpPut]
    [CustomAuthorize(UserType.Admin)]
    public async Task<ApiResultBase<bool>> PromoteWorker(string workerLogin)
    {
        return await _accountService.PromoteUser(workerLogin);
    }

    [HttpPut]
    [CustomAuthorize(UserType.Admin)]
    public async Task<ApiResultBase<bool>> DemoteWorker(string workerLogin)
    {
        return await _accountService.DemoteUser(workerLogin);
    }
}
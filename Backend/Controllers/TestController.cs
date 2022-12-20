using Backend.Utils;
using DataRepository.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TestController : ControllerBase
{

    [HttpGet]
    public IActionResult GetPublic()
    {
        return Ok("Public string");
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetForLogged()
    {
        return Ok("String for all logged");
    }

    [HttpGet]
    [CustomAuthorize(UserType.Worker | UserType.Admin)]
    public IActionResult GetForWorker()
    {
        return Ok("This can be seen by workers but also admins");
    }
    
    [HttpGet]
    [CustomAuthorize(UserType.Admin)]
    public IActionResult GetForAdmin()
    {
        return Ok("String only for admins");
    }
}
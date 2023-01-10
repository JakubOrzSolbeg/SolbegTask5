
using Backend.Services.Interfaces;
using Backend.Utils;
using Dtos.Requests;
using Dtos.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dtos.Enums;

namespace Backend.Controllers;

[ApiController]
[Route("/[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IShopService _shopService;
    public OrdersController(ITokenService tokenService, IShopService shopService)
    {
        _tokenService = tokenService;
        _shopService = shopService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ApiResultBase<List<OrderOverview>>>> GetOrders()
    {
        int userId = _tokenService.GetUserId(HttpContext);
        return Ok(await _shopService.GetUserOrders(userId));
    }
    
    [HttpGet]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<List<PendingOrderInfo>>> GetPendingOrders()
    {
        var result = await _shopService.GetPendingOrders();
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResultBase<bool>>> PlaceOrder(OrderRequest request)
    {
        int userId = _tokenService.GetUserId(HttpContext);
        var result = await _shopService.MakeOrder(userId, request.Products);
        return Ok(result);
    }

    [HttpPost]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> RejectOrder(int orderId)
    {
        return Ok(await _shopService.RejectOrder(orderId));
    }

    [HttpPost]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> AcceptOrder(int orderId)
    {
        return Ok(await _shopService.AcceptOrder(orderId));
    }
}
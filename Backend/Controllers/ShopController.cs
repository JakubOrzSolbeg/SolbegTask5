using Backend.Services.Interfaces;
using Backend.Utils;
using DataRepository.Entities;
using DataRepository.Enums;
using DataRepository.Repositories.Interfaces;
using Dtos.Requests;
using Dtos.Results;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ShopController : ControllerBase
{
    private readonly IShopService _shopService;
    public ShopController(IShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<List<ProductOverview>>>> Products()
    {
        return await _shopService.GetAllProducts();
    }

    [HttpPost]
    public async Task<ActionResult<ApiResultBase<List<ProductOverview>>>> ShopCart(List<int> products)
    {
        return await _shopService.GetShoppingCartProducts(products);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<ProductDetails>>> GetProductDetails(int productId)
    {
        return await _shopService.GetProductDetails(productId);
    }

    [HttpPost]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> AddProduct(NewProductCredentials productCredentials)
    {
        return await _shopService.AddProduct(productCredentials);
    }

    [HttpPost]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> AddBrand(string brandName)
    {
        return await _shopService.AddBrand(brandName);
    }

    [HttpDelete]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> RemoveProduct(int productId)
    {
        return await _shopService.RemoveProduct(productId);
    }

}
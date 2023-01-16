using Backend.Utils;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Dtos.Enums;
using Dtos.Requests;
using Dtos.Results;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

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
    public async Task<ActionResult<ApiResultBase<List<ProductOverview>>>> Products(ProductSearchParams searchParams)
    {
        return await _shopService.SearchProducts(searchParams);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResultBase<List<ProductOverview>>>> ShopCart(List<int> products)
    {
        return await _shopService.GetShoppingCartProducts(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResultBase<ProductDetails>>> Products(int id)
    {
        return await _shopService.GetProductDetails(id);
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

    [HttpPost]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> AddCategory(string categoryName)
    {
        return await _shopService.AddCategory(categoryName);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResultBase<List<CategoryDetail>>>> Categories()
    {
        return await _shopService.GetCategories();
    }

    [HttpGet]
    public async Task<ApiResultBase<List<BrandDetail>>> Brands()
    {
        return await _shopService.GetBrands();
    }

    [HttpGet]
    public async Task<ApiResultBase<ShopDetails>> ShopDetails()
    {
        return await _shopService.GetShopDetails();
    }

    [HttpDelete]
    [CustomAuthorize(UserType.Worker)]
    public async Task<ActionResult<ApiResultBase<bool>>> RemoveProduct(int productId)
    {
        return await _shopService.RemoveProduct(productId);
    }

}
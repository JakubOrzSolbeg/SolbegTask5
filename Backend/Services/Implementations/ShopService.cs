using Backend.Services.Interfaces;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Dtos.Requests;
using Dtos.Results;

namespace Backend.Services.Implementations;

public class ShopService : IShopService
{
    private readonly ExtendedRepository<Product> _productRepository;
    private readonly Repository<Brand> _brandRepository;
    public ShopService(ExtendedRepository<Product> productRepository, Repository<Brand> brandRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }
    
    public async Task<ApiResultBase<List<ProductOverview>>> GetAllProducts()
    {
        var result = await _productRepository.GetAllAndSelect(product => new ProductOverview()
        {
            ProductId = product.Id,
            PhotoUrl = product.PhotoUrl,
            Price = product.Cost,
            ProductName = product.Name
        });
        return new ApiResultBase<List<ProductOverview>>()
        {
            Body = result,
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<List<ProductOverview>>> GetShoppingCartProducts(List<int> productIds)
    {
        var cartProducts = await _productRepository.GetRange(productIds);
        var result = cartProducts.Select(product => new ProductOverview()
        {
            ProductId = product.Id,
            PhotoUrl = product.PhotoUrl,
            Price = product.Cost,
            ProductName = product.Name
        }).ToList();
        return new ApiResultBase<List<ProductOverview>>()
        {
            Body = result,
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<ProductDetails>> GetProductDetails(int productId)
    {
        var product = await _productRepository.GetById(productId);
        if (product == null)
        {
            return new ApiResultBase<ProductDetails>()
            {
                IsSuccess = false,
                Errors = "Product does not exists"
            };
        }

        var brand = await _brandRepository.GetById(product.BrandId);

        var result = new ProductDetails()
        {
            ProductId = product.Id,
            ProductName = product.Name,
            ProductDetail = product.Description,
            PhotoUrl = product.PhotoUrl,
            Price = product.Cost,
            Brand = brand?.Name?? "Unknown"
        };

        return new ApiResultBase<ProductDetails>()
        {
            IsSuccess = true,
            Body = result
        };
    }

    public async Task<ApiResultBase<bool>> AddProduct(NewProductCredentials newProductCredentials)
    {
        var brand = await _brandRepository.GetById(newProductCredentials.BrandId);

        if (brand == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Brand does not exists"
            };
        }
        
        var newProductEntity = new Product()
        {
            Name = newProductCredentials.ProductName,
            Description = newProductCredentials.ProductDetail,
            Cost = newProductCredentials.Price,
            BrandId = newProductCredentials.BrandId,
            PhotoUrl = newProductCredentials.PhotoUrl
        };
        await _productRepository.Add(newProductEntity);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<bool>> AddBrand(string brandName)
    {
        await _brandRepository.Add(new Brand()
        {
            Name = brandName
        });
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }

    public async Task<ApiResultBase<bool>> RemoveProduct(int productId)
    {
        var productForRemoval = await _productRepository.GetById(productId);
        if (productForRemoval == null)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Product does not exists"
            };
        }
        
        await _productRepository.Delete(productForRemoval);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true
        };
    }
}
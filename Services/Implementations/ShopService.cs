using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Dtos.Enums;
using Dtos.Requests;
using Dtos.Results;
using Services.Interfaces;

namespace Services.Implementations;

public class ShopService : IShopService
{
    private readonly ExtendedRepository<Product> _productRepository;
    private readonly ExtendedRepository<Brand> _brandRepository;
    private readonly NestedRepository<Order> _orderRepository;
    private readonly ExtendedRepository<Category> _categoryRepository;
    public ShopService(
        ExtendedRepository<Product> productRepository, 
        ExtendedRepository<Brand> brandRepository, 
        NestedRepository<Order> orderRepository,
        ExtendedRepository<Category> categoryRepository
        )
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _categoryRepository = categoryRepository;
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

    public async Task<ApiResultBase<List<ProductOverview>>> SearchProducts(ProductSearchParams searchParams)
    {
        var result = (await _productRepository.GetAllByPredicate(product =>
                product.Cost <= searchParams.MaxPrice &&
                product.Cost >= searchParams.MinPrice &&
                (searchParams.Categories.Count == 0 || searchParams.Categories.Contains(product.CategoryId)) &&
                (searchParams.Brands.Count == 0 || searchParams.Brands.Contains(product.BrandId)) &&
                (string.IsNullOrEmpty(searchParams.Name) ||
                 product.Name.ToLower().StartsWith(searchParams.Name!.ToLower()))
            ))
            .Select(product => new ProductOverview()
            {
                ProductName = product.Name,
                Price = product.Cost,
                PhotoUrl = product.PhotoUrl,
                ProductId = product.Id
            })
            .ToList();
        return new ApiResultBase<List<ProductOverview>>()
        {
            IsSuccess = true,
            Body = result
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
        var brand = 
            await _brandRepository.GetByPredicate(brand => 
                brand.Name.Equals(newProductCredentials.Brand)) ??
            await _brandRepository.Add(new Brand()
        {
            Name = newProductCredentials.Brand
        });
        
        var category =
            await _categoryRepository.GetByPredicate(category =>
                category.Name.Equals(newProductCredentials.Category)) ??
            await _categoryRepository.Add(new Category()
            {
                Name = newProductCredentials.Category
            });
        
        var newProductEntity = new Product()
        {
            Name = newProductCredentials.ProductName,
            Description = newProductCredentials.ProductDetail,
            Cost = newProductCredentials.Price,
            BrandId = brand.Id,
            CategoryId = category.Id,
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
            IsSuccess = true,
            Body = true
        };
    }

    public async Task<ApiResultBase<bool>> AddCategory(string categoryName)
    {
        await _categoryRepository.Add(new Category()
        {
            Name = categoryName
        });
        return new ApiResultBase<bool>()
        {
            IsSuccess = true,
            Body = true
        };
    }

    public async Task<ApiResultBase<List<CategoryDetail>>> GetCategories()
    {
        var result = (await _categoryRepository.GetAll()).Select(c => new CategoryDetail()
        {
            CategoryName = c.Name,
            CategoryId = c.Id
        }).ToList();
        return new ApiResultBase<List<CategoryDetail>>()
        {
            IsSuccess = true,
            Body = result
        };
    }

    public async Task<ApiResultBase<List<BrandDetail>>> GetBrands()
    {
        return new ApiResultBase<List<BrandDetail>>()
        {
            IsSuccess = true,
            Body = (await _brandRepository.GetAll()).Select(b => new BrandDetail()
            {
                BrandId = b.Id,
                BrandName = b.Name
            }).ToList()
        };
    }

    public async Task<ApiResultBase<ShopDetails>> GetShopDetails()
    {
        return new ApiResultBase<ShopDetails>()
        {
            IsSuccess = true,
            Body = new ShopDetails()
            {
                Categories = (await _categoryRepository.GetAll()).Select(c => new CategoryDetail()
                {
                    CategoryName = c.Name,
                    CategoryId = c.Id
                }).ToList(),
                Brands = (await _brandRepository.GetAll()).Select(b => new BrandDetail()
                {
                    BrandId = b.Id,
                    BrandName = b.Name
                }).ToList()
            }
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

    public async Task<ApiResultBase<bool>> MakeOrder(int userId, List<int> products)
    {
        if (products.Count < 1)
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Order must contain products"
            };
        }

        if (products.Any(p => p < 1))
        {
            return new ApiResultBase<bool>()
            {
                IsSuccess = false,
                Errors = "Illegal product Ids"
            };
        }
        
        Order placedOrder = new Order()
        {
            UserId = userId,
            Status = OrderStatus.Pending,
            OrderTime = DateTime.Now,
        };

        placedOrder.OrderProducts = products.Select(id => new OrderProduct()
        {
            Order = placedOrder,
            ProductId = id
        }).ToList();
        await _orderRepository.Add(placedOrder);
        return new ApiResultBase<bool>()
        {
            IsSuccess = true,
            Body = true
        };
    }
    
    public async Task<ApiResultBase<bool>> RejectOrder(int orderId)
    {
        Order? order = await _orderRepository.GetById(orderId);
        if (order == null)
        {
            return new ApiResultBase<bool>()
            {
                Errors = "Order does not exists",
                IsSuccess = false
            };
        }

        if (order.Status != OrderStatus.Pending)
        {
            return new ApiResultBase<bool>()
            {
                Errors = "Order already has been processed",
                IsSuccess = false
            };
        }

        order.Status = OrderStatus.Rejected;
        await _orderRepository.Update(order);

        return new ApiResultBase<bool>()
        {
            Body = true,
            IsSuccess = true
        };
        
    }

    public async Task<ApiResultBase<bool>> AcceptOrder(int orderId)
    {
        {
            Order? order = await _orderRepository.GetById(orderId);
            if (order == null)
            {
                return new ApiResultBase<bool>()
                {
                    Errors = "Order does not exists",
                    IsSuccess = false
                };
            }

            if (order.Status != OrderStatus.Pending)
            {
                return new ApiResultBase<bool>()
                {
                    Errors = "Order already has been processed",
                    IsSuccess = false
                };
            }

            order.Status = OrderStatus.Accepted;
            await _orderRepository.Update(order);

            return new ApiResultBase<bool>()
            {
                Body = true,
                IsSuccess = true
            };
        }
    }

    public async Task<ApiResultBase<OrderDetails>> GetOrderDetails(int orderId)
    {
        var result = await _orderRepository.GetOneNested(orderId, order => new OrderDetails()
        {
            OrderId = order.Id,
            Status = order.Status,
            Products = order.OrderProducts.Select(op => op.Product).Select(product => new ProductOverview()
            {
                ProductName = product.Name,
                Price = product.Cost,
                ProductId = product.Id,
                PhotoUrl = product.PhotoUrl
            }).ToList()
        });

        if (result == null)
        {
            return new ApiResultBase<OrderDetails>()
            {
                Errors = "Order is empty",
                IsSuccess = false
            };
        }
        else
        {
            return new ApiResultBase<OrderDetails>()
            {
                IsSuccess = true,
                Body = result
            };
        }
    }

    public async Task<ApiResultBase<List<OrderOverview>>> GetUserOrders(int userId)
    {
        var result = await _orderRepository.GetNestedByPredicate(order => order.UserId.Equals(userId), order => new OrderOverview()
        {
            Status = order.Status,
            OrderId = order.Id,
            OrderDate = order.OrderTime
        });
        return new ApiResultBase<List<OrderOverview>>()
        {
            IsSuccess = true,
            Body = result
        };
    }

    public async Task<ApiResultBase<List<PendingOrderInfo>>> GetPendingOrders()
    {
        var result = await _orderRepository.GetNestedByPredicate(
            order => order.Status == OrderStatus.Pending,
            order => new PendingOrderInfo()
            {
                UserId = order.UserId,
                OrderId = order.Id,
                ProductNames = Enumerable.ToList<string>(order.OrderProducts.Select(p => p.Product.Name))
            });

        var apiResult = new ApiResultBase<List<PendingOrderInfo>>()
        {
            IsSuccess = true,
            Body = result
        };
        return apiResult;
    }
}

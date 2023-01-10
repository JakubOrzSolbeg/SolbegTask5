using Dtos.Requests;
using Dtos.Results;

namespace Backend.Services.Interfaces;

public interface IShopService
{
    public Task<ApiResultBase<List<ProductOverview>>> GetAllProducts();
    public Task<ApiResultBase<List<ProductOverview>>> GetShoppingCartProducts(List<int> productIds);
    public Task<ApiResultBase<ProductDetails>> GetProductDetails(int productId);
    public Task<ApiResultBase<bool>> AddProduct(NewProductCredentials newProductCredentials);
    public Task<ApiResultBase<bool>> AddBrand(string brandName);
    public Task<ApiResultBase<bool>> RemoveProduct(int productId);
    public Task<ApiResultBase<bool>> MakeOrder(int userId, List<int> products);
    public Task<ApiResultBase<bool>> RejectOrder(int orderId);
    public Task<ApiResultBase<bool>> AcceptOrder(int orderId);
    public Task<ApiResultBase<OrderDetails>> GetOrderDetails(int orderId);
    public Task<ApiResultBase<List<OrderOverview>>> GetUserOrders(int userId);
    public Task<ApiResultBase<List<PendingOrderInfo>>> GetPendingOrders();
}
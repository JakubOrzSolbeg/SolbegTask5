using Dtos.Enums;

namespace Dtos.Results;

public class OrderDetails
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public List<ProductOverview> Products { get; set; }
}
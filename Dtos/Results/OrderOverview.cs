using Dtos.Enums;

namespace Dtos.Results;

public class OrderOverview
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime OrderDate { get; set; }
}
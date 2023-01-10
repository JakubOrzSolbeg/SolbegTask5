namespace Dtos.Results;

public class PendingOrderInfo
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public List<string> ProductNames { get; set; }
}
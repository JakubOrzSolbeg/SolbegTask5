namespace Dtos.Results;

public class ProductOverview
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public double Price { get; set; }
    public string PhotoUrl { get; set; } = String.Empty;
}
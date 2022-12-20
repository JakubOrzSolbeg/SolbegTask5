namespace Dtos.Results;

public class ProductDetails
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public string ProductDetail { get; set; } = String.Empty;
    public double Price { get; set; }
    public string PhotoUrl { get; set; } = String.Empty;
    public string Brand { get; set; } = null!;
}
namespace Dtos.Requests;

public class NewProductCredentials
{
    public string ProductName { get; set; } = null!;
    public string ProductDetail { get; set; } = String.Empty;
    public double Price { get; set; }
    public string PhotoUrl { get; set; } = String.Empty;
    public string Brand { get; set; } = "Unknown";
    public string Category { get; set; } = "Unknown";
}
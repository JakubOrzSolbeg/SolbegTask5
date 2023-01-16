namespace Dtos.Requests;

public class ProductSearchParams
{
    public int MinPrice { get; set; } = 0;
    public int MaxPrice { get; set; } = Int32.MaxValue;
    public string Name { get; set; } = String.Empty;
    public List<int> Categories { get; set; } = new List<int>();
    public List<int> Brands { get; set; } = new List<int>();
}
using System.Text.Json.Serialization;

namespace Dtos.Requests;

public class OrderRequest
{
    [JsonPropertyName("productIds")]
    public List<int> Products { get; set; }
}
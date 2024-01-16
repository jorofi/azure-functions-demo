using System.Text.Json.Serialization;

namespace FunctionsDemo.Abstractions
{
    public record Order
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("storeId")]
        public Guid StoreId { get; set; }

        [JsonPropertyName("productName")]
        public string ProductName { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; } = 0;

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 0;


    }
}

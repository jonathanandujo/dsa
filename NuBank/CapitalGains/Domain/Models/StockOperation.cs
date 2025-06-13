namespace Domain.Models;
public record StockOperation
{
    [JsonPropertyName("operation")]
    public required string Operation { get; set; }

    [JsonPropertyName("unit-cost")]
    public required decimal UnitCost { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }
}
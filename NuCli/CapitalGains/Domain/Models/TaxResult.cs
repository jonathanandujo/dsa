namespace Domain.Models;
public class TaxResult
{
    [JsonPropertyName("tax")]
    [JsonConverter(typeof(DecimalConverter))]
    public decimal Tax { get; set; }
}
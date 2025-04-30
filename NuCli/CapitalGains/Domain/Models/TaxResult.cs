namespace Domain.Models;

public record TaxResult
{
    [JsonPropertyName("tax")]
    [JsonConverter(typeof(DecimalConverter))]
    public decimal Tax { get; set; }
}

//Tax0 can be used when buying that means tax is 0 or when amount is lower than min amount for taxes
public record Tax0 : TaxResult
{
    public Tax0() : base()
    {
        Tax = 0m;
    }
}
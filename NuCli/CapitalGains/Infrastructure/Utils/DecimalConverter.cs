namespace Infrastructure.Utils;

public class DecimalConverter : JsonConverter<decimal>
{
    public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.GetDecimal();
    }

    public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
    {
        //if we want 2 decimals in the output, this needs to be replaced by f2
        writer.WriteStringValue(value.ToString("f1"));
    }
}

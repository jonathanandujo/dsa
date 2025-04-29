using System.Text.Json;
using Domain.Models;

namespace Infrastructure;

public static class JsonParser
{
    public static List<StockOperation>? ParseStockOperations(string json)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var stockOperations = JsonSerializer.Deserialize<List<StockOperation>>(json, options);

            if (stockOperations == null || stockOperations.Count == 0)
            {
                ConsoleHelper.WriteError("Data does not contains operations.");
                return null;
            }

            return stockOperations;
        }
        catch (JsonException)
        {
            ConsoleHelper.WriteError("Line not valid, please ensure it has correct format.");
            return null;
        }
    }
}

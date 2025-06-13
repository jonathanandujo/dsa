using System.Text.Json;
using Domain.Models;
using Infrastructure;

namespace Infrastructure.Services;

public class StockTaxService
{
    public void ProcessStockOperations(string jsonInput)
    {
        var stockOperations = JsonParser.ParseStockOperations(jsonInput);

        if (stockOperations == null)
            return;

        TaxCalculatorAdapter taxCalculatorService = new();

        try
        {
            var results = taxCalculatorService.CalculateTaxes(stockOperations);

            if (results != null)
                ConsoleHelper.WriteSuccess(JsonSerializer.Serialize(results));
        }
        catch(Exception ex)
        {
            if(GlobalConfig.Debug)
                ConsoleHelper.WriteInfo($"{ex}");
        }
    }
}

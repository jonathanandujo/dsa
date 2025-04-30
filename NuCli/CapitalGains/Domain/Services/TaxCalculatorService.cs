using Application;
using Domain.Models;

namespace Domain.Services;

public class TaxCalculatorService : ITaxCalculator
{
    private decimal weightedAvgPrice = 0;
    private int stockQuantity = 0;
    private decimal accumulatedLosses = 0;

    //Time complexity O(n) we validate only once each operation, saving weighted average price and quantity of stocks.
    public List<TaxResult> CalculateTaxes(List<StockOperation> operations)
    {
        List<TaxResult> results = new();

        foreach (StockOperation op in operations)
        {
            switch(op.Operation.ToLower())
            {
                case "buy":
                    weightedAvgPrice = ((stockQuantity * weightedAvgPrice) + (op.Quantity * op.UnitCost)) / (stockQuantity + op.Quantity);
                    stockQuantity += op.Quantity;
                    results.Add(new Tax0());
                    break;
                case "sell":
                    decimal totalAmount = op.UnitCost * op.Quantity;
                    decimal profit = (op.UnitCost - weightedAvgPrice) * op.Quantity;

                    if (profit < 0)
                    {
                        accumulatedLosses += Math.Abs(profit);
                        results.Add(new Tax0());
                    }
                    else
                    {
                        if (totalAmount > GlobalConfig.MinTaxableAmount)
                        {
                            decimal taxableProfit = Math.Max(0, profit - accumulatedLosses);
                            accumulatedLosses = Math.Max(0, accumulatedLosses - profit);
                            results.Add(new TaxResult { Tax = taxableProfit * GlobalConfig.TaxRate });
                        }
                        else
                        {
                            results.Add(new Tax0());
                        }
                    }
                    stockQuantity -= op.Quantity;
                    if(stockQuantity < 0)
                    {
                        ConsoleHelper.WriteError($"Cannot sell more stocks than you own.");
                        throw new Exception("The operation are trying to sell more stocks that owning");
                    }
                break;
            }
        }
        return results;
    }
}
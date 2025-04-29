using Application;
using Domain.Models;

namespace Domain.Services;

public class TaxCalculatorService : ITaxCalculator
{
    private decimal weightedAvgPrice = 0;
    private int stockQuantity = 0;
    private decimal accumulatedLosses = 0;

    public List<TaxResult> CalculateTaxes(List<StockOperation> operations)
    {
        List<TaxResult> results = new();

        foreach (var op in operations)
        {
            if (op.Operation.ToLower() == "buy")
            {
                weightedAvgPrice = ((stockQuantity * weightedAvgPrice) + (op.Quantity * op.UnitCost)) 
                                   / (stockQuantity + op.Quantity);
                stockQuantity += op.Quantity;
                results.Add(new TaxResult { Tax = 0 });
            }
            else if (op.Operation.ToLower() == "sell")
            {
                decimal totalAmount = op.UnitCost * op.Quantity;
                decimal profit = (op.UnitCost - weightedAvgPrice) * op.Quantity;

                if (profit < 0)
                {
                    accumulatedLosses += Math.Abs(profit);
                    results.Add(new TaxResult { Tax = 0 });
                }
                else
                {
                    if (totalAmount > 20000)
                    {
                        decimal taxableProfit = Math.Max(0, profit - accumulatedLosses);
                        accumulatedLosses -= profit;
                        results.Add(new TaxResult { Tax = taxableProfit * GlobalConfig.TaxRate });
                    }
                    else
                    {
                        results.Add(new TaxResult { Tax = 0 });
                    }
                }

                stockQuantity -= op.Quantity;
            }
        }

        return results;
    }
}

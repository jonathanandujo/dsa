using Application;
using Domain.Models;
using Domain.Services;

namespace Infrastructure;

public class TaxCalculatorAdapter : ITaxCalculator
{
    private readonly TaxCalculatorService _service = new();

    public List<TaxResult> CalculateTaxes(List<StockOperation> operations)
    {
        return _service.CalculateTaxes(operations);
    }
}

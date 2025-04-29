using Domain.Models;

namespace Application;

public interface ITaxCalculator
{
    List<TaxResult> CalculateTaxes(List<StockOperation> operations);
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Models;
using Domain.Services;
using System.Collections.Generic;

namespace Tests;
[TestClass]
public class TaxCalculatorServiceTests
{
    private readonly TaxCalculatorService _service = new();

    [TestMethod]
    public void CalculateTaxes_SimpleBuySell_ShouldApplyCorrectTax()
    {
        var operations = new List<StockOperation>
        {
            new StockOperation { Operation = "buy", UnitCost = 10.00m, Quantity = 10000 },
            new StockOperation { Operation = "sell", UnitCost = 20.00m, Quantity = 5000 }
        };

        var results = _service.CalculateTaxes(operations);

        Assert.IsNotNull(results);
        Assert.AreEqual(2, results.Count);
        Assert.IsTrue(results[1].Tax >= 0);
    }
}
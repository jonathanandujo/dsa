using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Infrastructure.Services;

namespace Tests;
[TestClass]
public class StockTaxServiceTests
{
    private StockTaxService _service = new();

    [TestInitialize]
    public void Setup()
    {
    }

    [TestMethod]
    public void ProcessStockOperations_ShouldPrint_ValidTaxResults()
    {
        string jsonInput = "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":10000},"
                            + "{\"operation\":\"sell\",\"unit-cost\":20.00,\"quantity\":5000}]";

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);  // Redirect console output

            _service.ProcessStockOperations(jsonInput);

            string output = sw.ToString().Trim();
            Assert.IsFalse(string.IsNullOrWhiteSpace(output), "Output should not be empty");
            Assert.IsTrue(output.Contains("\"tax\":"), "Output should contain tax results");
        }
    }

    [TestMethod]
    public void ProcessStockOperations_InvalidJson_ShouldNotPrintAnything()
    {
        string invalidJson = "{operation:buy, unit-cost:10.00, quantity:10000}";

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            _service.ProcessStockOperations(invalidJson);

            string output = sw.ToString().Trim();
            Assert.AreEqual(output, "Line not valid, please ensure it has correct format.");
        }
    }
}
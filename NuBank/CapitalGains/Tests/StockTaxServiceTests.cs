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

    //This tests contains all the examples of the requirement doc
    [TestMethod]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\":50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\":50}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\":5000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"10000.0\"},{\"tax\":\"0.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":3000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"1000.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\":10000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\":5000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"10000.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":2000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":2000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\":1000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"3000.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":2000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\":2000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\":1000},{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\":5000},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\":4350},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\":650}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"3000.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"3700.0\"},{\"tax\":\"0.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\":10000},{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\":10000},{\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\":10000}]", "[{\"tax\":\"0.0\"},{\"tax\":\"80000.0\"},{\"tax\":\"0.0\"},{\"tax\":\"60000.0\"}]")]
    [DataRow("[{\"operation\":\"buy\", \"unit-cost\":5000.00, \"quantity\":10},{\"operation\":\"sell\", \"unit-cost\":4000.00, \"quantity\":5},{\"operation\":\"buy\", \"unit-cost\":15000.00, \"quantity\":5},{\"operation\":\"buy\", \"unit-cost\":4000.00, \"quantity\":2},{\"operation\":\"buy\", \"unit-cost\":23000.00, \"quantity\":2},{\"operation\":\"sell\", \"unit-cost\":20000.00, \"quantity\":1},{\"operation\":\"sell\", \"unit-cost\":12000.00, \"quantity\":10},{\"operation\":\"sell\", \"unit-cost\":15000.00, \"quantity\":3}]", "[{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"0.0\"},{\"tax\":\"1000.0\"},{\"tax\":\"2400.0\"}]")]
    public void ProcessStockOperations_ValidJson_ExamplesFromRequirementDoc(string jsonInput, string expectedOutput)
    {
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
            _service.ProcessStockOperations(jsonInput);
            string output = sw.ToString().Trim();
            Assert.AreEqual(expectedOutput, output, "Tax calculation output does not match expected result.");
        }
    }

    [TestMethod]
    public void ProcessStockOperations_ShouldPrint_ValidTaxResults()
    {
        string jsonInput = "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":10000},"+ "{\"operation\":\"sell\",\"unit-cost\":20.00,\"quantity\":5000}]";

        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
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
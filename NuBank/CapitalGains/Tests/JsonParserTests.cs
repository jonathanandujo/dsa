using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;

namespace Tests;
[TestClass]
public class JsonParserTests
{
    [TestMethod]
    public void ParseStockOperations_ValidJson_ShouldDeserialize()
    {
        string json = "[{\"operation\":\"buy\",\"unit-cost\":10.00,\"quantity\":10000}]";

        var operations = JsonParser.ParseStockOperations(json);

        Assert.IsNotNull(operations);
        Assert.AreEqual(1, operations.Count);
        Assert.AreEqual("buy", operations[0].Operation);
    }

    [TestMethod]
    public void ParseStockOperations_InvalidJson_ShouldReturnNull()
    {
        string invalidJson = "{operation:buy, unit-cost:10.00, quantity:10000}";

        var operations = JsonParser.ParseStockOperations(invalidJson);

        Assert.IsNull(operations);
    }
}
using Application;
using Domain.Models;

namespace Infrastructure;

public class StockRepository : IStockRepository
{
    public StockOperation GetStock(string symbol)
    {
        return new StockOperation { Operation = "buy", UnitCost = 100, Quantity = 10 };
    }
}

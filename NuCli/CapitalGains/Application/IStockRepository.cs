using Domain.Models;

namespace Application;

public interface IStockRepository
{
    StockOperation GetStock(string symbol);
}

using Orleans;

namespace MicrosoftOrleans;

public interface ICounterGrain : IGrainWithStringKey
{
    Task<int> GetCountAsync();
    Task<int> IncrementAsync(int amount = 1);
    Task<int> DecrementAsync(int amount = 1);
    Task ResetAsync();
    Task<CounterState> GetStateAsync();
}

[GenerateSerializer]
public class CounterState
{
    [Id(0)] public int Count { get; set; }
    [Id(1)] public DateTime LastUpdated { get; set; }
    [Id(2)] public int TotalOperations { get; set; }
    
    public CounterState() { } // Parameterless constructor required
    
    public CounterState(int count, DateTime lastUpdated, int totalOperations)
    {
        Count = count;
        LastUpdated = lastUpdated;
        TotalOperations = totalOperations;
    }
}
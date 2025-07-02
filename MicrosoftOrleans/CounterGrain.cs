using Orleans;

namespace MicrosoftOrleans;

public class CounterGrain : Grain, ICounterGrain
{
    private int _count = 0;
    private DateTime _lastUpdated = DateTime.UtcNow;
    private int _totalOperations = 0;

    public Task<int> GetCountAsync()
    {
        return Task.FromResult(_count);
    }

    public Task<int> IncrementAsync(int amount = 1)
    {
        _count += amount;
        _lastUpdated = DateTime.UtcNow;
        _totalOperations++;
        
        // Log the operation (Orleans provides excellent logging integration)
        var logger = ServiceProvider.GetService<ILogger<CounterGrain>>();
        logger?.LogInformation("Counter {GrainKey} incremented by {Amount}. New value: {Count}", 
            this.GetPrimaryKeyString(), amount, _count);
        
        return Task.FromResult(_count);
    }

    public Task<int> DecrementAsync(int amount = 1)
    {
        _count -= amount;
        _lastUpdated = DateTime.UtcNow;
        _totalOperations++;
        
        var logger = ServiceProvider.GetService<ILogger<CounterGrain>>();
        logger?.LogInformation("Counter {GrainKey} decremented by {Amount}. New value: {Count}", 
            this.GetPrimaryKeyString(), amount, _count);
        
        return Task.FromResult(_count);
    }

    public Task ResetAsync()
    {
        _count = 0;
        _lastUpdated = DateTime.UtcNow;
        _totalOperations++;
        
        var logger = ServiceProvider.GetService<ILogger<CounterGrain>>();
        logger?.LogInformation("Counter {GrainKey} reset", this.GetPrimaryKeyString());
        
        return Task.CompletedTask;
    }

    public Task<CounterState> GetStateAsync()
    {
        return Task.FromResult(new CounterState(_count, _lastUpdated, _totalOperations));
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var logger = ServiceProvider.GetService<ILogger<CounterGrain>>();
        logger?.LogInformation("Counter grain {GrainKey} activated", this.GetPrimaryKeyString());
        return base.OnActivateAsync(cancellationToken);
    }

    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        var logger = ServiceProvider.GetService<ILogger<CounterGrain>>();
        logger?.LogInformation("Counter grain {GrainKey} deactivated. Reason: {Reason}", 
            this.GetPrimaryKeyString(), reason);
        return base.OnDeactivateAsync(reason, cancellationToken);
    }
}
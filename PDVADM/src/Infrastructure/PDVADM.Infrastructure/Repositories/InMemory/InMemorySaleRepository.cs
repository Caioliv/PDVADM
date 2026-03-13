using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Infrastructure.Repositories.InMemory;

public sealed class InMemorySaleRepository : ISaleRepository
{
    private long _nextId = 1;

    public Task<long> CreateAsync(Guid userId, decimal discount, string? notes)
    {
        var id = _nextId++;
        return Task.FromResult(id);
    }

    public Task AddItemAsync(long saleId, long productId, int quantity, decimal unitPrice)
    {
        return Task.CompletedTask;
    }
}


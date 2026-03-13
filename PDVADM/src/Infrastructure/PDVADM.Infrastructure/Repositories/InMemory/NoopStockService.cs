using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Infrastructure.Repositories.InMemory;

public sealed class NoopStockService : IStockService
{
    public Task DecreaseAsync(long productId, int quantity, string reason)
    {
        return Task.CompletedTask;
    }
}


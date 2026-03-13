using PDVADM.Application.Services.Sales;

namespace PDVADM.Api.Services.Sales;

public sealed class NoopStockService : IStockService
{
    public Task DecreaseAsync(long productId, int quantity, string reason)
    {
        return Task.CompletedTask;
    }
}


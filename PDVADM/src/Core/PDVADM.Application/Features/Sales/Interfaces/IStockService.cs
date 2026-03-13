namespace PDVADM.Application.Features.Sales.Interfaces;

public interface IStockService
{
    Task DecreaseAsync(long productId, int quantity, string reason);
}


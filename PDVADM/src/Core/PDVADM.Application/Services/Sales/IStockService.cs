namespace PDVADM.Application.Services.Sales;

public interface IStockService
{
    Task DecreaseAsync(long productId, int quantity, string reason);
}


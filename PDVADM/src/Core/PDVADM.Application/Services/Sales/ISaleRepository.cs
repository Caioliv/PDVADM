namespace PDVADM.Application.Services.Sales;

public interface ISaleRepository
{
    Task<long> CreateAsync(Guid userId, decimal discount, string? notes);
    Task AddItemAsync(long saleId, long productId, int quantity, decimal unitPrice);
}


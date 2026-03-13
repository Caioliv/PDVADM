using Dapper;
using PDVADM.Application.Features.Sales.Interfaces;
using PDVADM.Infrastructure.Database;

namespace PDVADM.Infrastructure.Repositories.Dapper;

public class SaleRepository : ISaleRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public SaleRepository(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<long> CreateAsync(Guid userId, decimal discount, string? notes)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"INSERT INTO sales (user_id, discount, notes, created_at, updated_at)
                    VALUES (@UserId, @Discount, @Notes, NOW(), NOW())
                    RETURNING id";

        return await connection.ExecuteScalarAsync<long>(sql, new
        {
            UserId = userId,
            Discount = discount,
            Notes = notes
        });
    }

    public async Task AddItemAsync(long saleId, long productId, int quantity, decimal unitPrice)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();

        var sql = @"INSERT INTO sale_items (sale_id, product_id, quantity, unit_price, created_at)
                    VALUES (@SaleId, @ProductId, @Quantity, @UnitPrice, NOW())";

        await connection.ExecuteAsync(sql, new
        {
            SaleId = saleId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        });
    }
}

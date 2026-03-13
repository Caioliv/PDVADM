using Dapper;
using PDVADM.Application.Features.Sales.Interfaces;
using System.Data;
using PDVADM.Infrastructure.Database;

namespace PDVADM.Infrastructure.Services;

public class StockService : IStockService
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StockService(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task DecreaseAsync(long productId, int quantity, string reason)
    {
        using var connection = await _dbConnectionFactory.CreateConnectionAsync();
        
        // SQL para baixar o estoque no Postgres
        // O "WHERE" garante que estamos mexendo no produto certo
        var sql = @"UPDATE stocks 
                    SET quantity = quantity - @Quantity, 
                        updated_at = NOW() 
                    WHERE product_id = @ProductId";
        
        await connection.ExecuteAsync(sql, new { 
            Quantity = quantity, 
            ProductId = productId 
        });
    }
}

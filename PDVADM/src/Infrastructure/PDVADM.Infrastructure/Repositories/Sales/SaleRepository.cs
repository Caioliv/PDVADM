using Dapper;
using System.Data;
using PDVADM.Infrastructure.Database;

public class SaleRepository : ISaleRepository
{
    private readonly DbConnectionFactory _connectionFactory;

    public SaleRepository(DbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<long> CreateSaleAsync(Guid userId, decimal discount, string? notes, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"
            INSERT INTO sales (user_id, discount, notes)
            VALUES (@UserId, @Discount, @Notes)
            RETURNING id;
        ";

        return await connection.ExecuteScalarAsync<long>(sql, new
        {
            UserId = userId,
            Discount = discount,
            Notes = notes
        }, transaction); // Passamos a transação aqui
    }

    public async Task AddItemAsync(long saleId, long productId, int quantity, decimal unitPrice, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"
            INSERT INTO sale_items (sale_id, product_id, quantity, unit_price)
            VALUES (@SaleId, @ProductId, @Quantity, @UnitPrice);
        ";

        await connection.ExecuteAsync(sql, new
        {
            SaleId = saleId,
            ProductId = productId,
            Quantity = quantity,
            UnitPrice = unitPrice
        }, transaction); // Passamos a transação aqui
    }
}
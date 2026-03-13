using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using PDVADM.Application.Features.Sales.Interfaces;

namespace PDVADM.Infrastructure.Database;

public class DbConnectionFactory : IDbConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("Postgres");
        return new NpgsqlConnection(connectionString);
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        var connection = (NpgsqlConnection)CreateConnection();
        await connection.OpenAsync();
        return connection;
    }
}

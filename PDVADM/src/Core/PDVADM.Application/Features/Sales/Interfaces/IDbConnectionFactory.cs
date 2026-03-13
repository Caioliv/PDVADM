using System.Data;

namespace PDVADM.Application.Features.Sales.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
    Task<IDbConnection> CreateConnectionAsync();
}

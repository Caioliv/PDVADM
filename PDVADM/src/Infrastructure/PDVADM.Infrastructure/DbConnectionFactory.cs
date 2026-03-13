using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace PDVADM.Infrastructure.Database
{
    public class DbConnectionFactory
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
    }
}

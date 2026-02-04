using Dapper;
using Microsoft.AspNetCore.Mvc;
using PDVADM.Infrastructure.Database;

namespace PDVADM.Controllers
{
    [ApiController]
    [Route("api/database-health")]
    public class DatabaseHealthController : ControllerBase
    {
        private readonly DbConnectionFactory _connectionFactory;

        public DatabaseHealthController(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var connection = _connectionFactory.CreateConnection();

            var dbTime = await connection.QuerySingleAsync<DateTime>(
                "SELECT NOW()"
            );

            return Ok(new
            {
                status = "ok",
                database_time = dbTime
            });
        }
    }
}

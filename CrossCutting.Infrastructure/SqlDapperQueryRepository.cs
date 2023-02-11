using CrossCutting.Domain.SeedWork;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CrossCutting.Infrastructure
{
    public class SqlDapperQueryRepository<T> : IQueryRepository<T>
        where T : class
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public SqlDapperQueryRepository(IConfiguration configuration, string connectionString)
        {
            this._configuration = configuration;
            this._connectionString = connectionString;
        }

        protected async Task<IReadOnlyList<T>> GetQuery(string query, object? parms = null)
        {
            using var conn = new SqlConnection(_connectionString);
            return (await conn.QueryAsync<T>(query, parms)).ToList();
        }


    }
}

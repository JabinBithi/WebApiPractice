using System;
using Oracle.ManagedDataAccess.Client;

namespace WebApplication1.Database
{
    public class OracleDbContext
    {
        private readonly IConfiguration _configuration;

        public OracleDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public OracleConnection CreateConnection()
        {
            var cs = _configuration.GetConnectionString("OracleDb");
            if (string.IsNullOrWhiteSpace(cs))
            {
                throw new InvalidOperationException("Connection string 'OracleDb' is not configured.");
            }

            return new OracleConnection(cs);
        }
    }
}
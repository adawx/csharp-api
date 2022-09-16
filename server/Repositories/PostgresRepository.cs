using System;
using Npgsql;
using System.Data;

namespace server.Repositories
{
    public class PostgreSqlRepository
    {
        private readonly string ConnectionString; 

        private readonly string DbSchema;

        public PostgreSqlRepository(string connectionString, string dbSchema)
        {
            ConnectionString = connectionString;
            DbSchema = dbSchema;
        }

        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(ConnectionString);
            return conn;
        }
    }
}


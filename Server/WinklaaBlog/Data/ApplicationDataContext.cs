using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace WinklaaBlog.Data
{
    public class ApplicationDataContext
    {
        private readonly IConfiguration _config;
        public ApplicationDataContext(IConfiguration config)
        {
            _config = config;
        }
        public IDbConnection DBConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            return DBConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            return DBConnection.QuerySingle<T>(sql);
        }

        public T LoadDataOrDefaultSingle<T>(string sql)
        {
            return DBConnection.QuerySingleOrDefault<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            return DBConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            return DBConnection.Execute(sql);
        }

        public bool ExecuteSqlWithParameters(string sql, IEnumerable<SqlParameter> parameters)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddRange(parameters.ToArray());

                connection.Open();

                var affectedRows = command.ExecuteNonQuery();
                return affectedRows > 0;
            }
        }
    }
}

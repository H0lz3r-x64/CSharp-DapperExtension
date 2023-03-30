using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace DapperExtension
{
    public class MySqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;
        public MySqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Receive
        public async Task<IEnumerable<T>> LoadDataORM<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<IDictionary<string, object>>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);

            IEnumerable<dynamic> results = await connection.QueryAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            List<IDictionary<string, object>> final = new List<IDictionary<string, object>>();

            foreach (var row in results)
                final.Add((IDictionary<string, object>)row);

            return final;
        }
        #endregion

        #region Send
        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new MySqlConnection(_connectionString);

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        #endregion
    }
}

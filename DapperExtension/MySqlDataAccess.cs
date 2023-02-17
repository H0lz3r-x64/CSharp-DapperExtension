using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace DapperExtension
{
    public class MySqlDataAccess : ISqlDataAccess
    {
        private readonly Configuration _config;
        public MySqlDataAccess(Configuration config)
        {
            _config = config;
        }

        #region Receive
        public async Task<IEnumerable<T>> LoadDataORM<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new MySqlConnection(_config.ConnectionStrings.ConnectionStrings[connectionId].ConnectionString);

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<List<IDictionary<string, object>>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new MySqlConnection(_config.ConnectionStrings.ConnectionStrings[connectionId].ConnectionString);

            IEnumerable<dynamic> results = await connection.QueryAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            List<IDictionary<string, object>> final = new List<IDictionary<string, object>>();

            foreach (var row in results)
                final.Add((IDictionary<string, object>)row);

            return final;
        }
        #endregion

        #region Send
        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default")
        {
            using IDbConnection connection = new MySqlConnection(_config.ConnectionStrings.ConnectionStrings[connectionId].ConnectionString);

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
        #endregion
    }
}

using Dapper;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace DapperExtension
{
    public class OtherSqlDataAccess : ISqlDataAccess
    {
        private readonly string _connectionString;
        public OtherSqlDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region Receive
        public async Task<IEnumerable<T>> LoadDataORM<T, U>(string storedProcedure, U parameters)
        {
            throw new NotSupportedException();
        }

        public async Task<List<IDictionary<string, object>>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            throw new NotSupportedException();
        }
        #endregion

        #region Send
        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            throw new NotSupportedException();
        }
        #endregion
    }
}

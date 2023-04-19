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
            CheckConnection();
        }

        #region Check Connection
        public bool CheckConnection()
        {
            bool isConn = false;
            MySqlConnection conn = null;
            try
            {
                conn = new MySqlConnection(_connectionString);
                conn.Open();
                isConn = true;
            }
            catch (ArgumentException a_ex)
            {
                Console.WriteLine("Check the Connection String.");
                Console.WriteLine(a_ex.Message);
                Console.WriteLine(a_ex.ToString());
                throw a_ex;
            }
            catch (MySqlException ex)
            {
                string sqlErrorMessage = "Message: " + ex.Message + "\n" +
                "Source: " + ex.Source + "\n" +
                "Number: " + ex.Number;
                Console.WriteLine(sqlErrorMessage);
                isConn = false;
                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return isConn;
        }
        #endregion

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

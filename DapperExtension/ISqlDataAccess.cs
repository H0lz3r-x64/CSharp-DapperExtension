namespace DapperExtension
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadDataORM<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task<List<IDictionary<string, object>>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
    }
}
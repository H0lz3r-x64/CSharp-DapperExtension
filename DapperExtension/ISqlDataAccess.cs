namespace DapperExtension
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> LoadDataORM<T, U>(string storedProcedure, U parameters);
        Task<List<IDictionary<string, object>>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
    }
}
using System.Reflection;

namespace DapperExtension
{

    //Repository pattern
    //Data Access Object (DAO) pattern

    public class DataAccessHandler
    {
        private readonly ISqlDataAccess _dbAccess;
        public DataAccessHandler(ISqlDataAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        #region StoredProcedures

        #region NonORM
        public async Task<List<IDictionary<string, object>>> sp_getByIdAsync(string storedProc, int id)
        {
            List<IDictionary<string, object>> results = await _dbAccess.LoadData<string, dynamic>(
            storedProcedure: storedProc,
            new { ID = id });
            return results;
        }

        public async Task<List<IDictionary<string, object>>> sp_getAllAsync(string storedProc)
        {
            List<IDictionary<string, object>> results = await _dbAccess.LoadData<string, dynamic>(
            storedProcedure: storedProc,
            new { });
            return results;
        }
        #endregion

        #region ORM
        public async Task<T?> sp_getByIdOrmAsync<T>(string storedProc, int id)
        {
            IEnumerable<T> results = await _dbAccess.LoadDataORM<T, dynamic>(
            storedProcedure: storedProc,
            new { Id = id });
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<T?>> sp_getAllOrmAsync<T>(string storedProc)
        {
            IEnumerable<T?> results = await _dbAccess.LoadDataORM<T?, dynamic>(
            storedProcedure: storedProc,
            new { });
            return results;
        }

        public async Task sp_insertOrmAsync<T>(string storedProc, T obj, bool ignorePK = true)
        {
            if (!ignorePK)
            {
                await _dbAccess.SaveData(storedProcedure: storedProc, obj);
                return;
            }
            dynamic newObj = obj;
            PropertyInfo[] propInf = obj.GetType().GetProperties();
            propInf[0].SetValue(newObj, null);

            await _dbAccess.SaveData(storedProcedure: storedProc, (T)newObj);
            return;
        }

        public async Task sp_updateOrmAsync<T>(string storedProc, T obj) =>
            await _dbAccess.SaveData(storedProc, obj);

        #endregion

        #endregion

    }
}

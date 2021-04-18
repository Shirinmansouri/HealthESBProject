using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace HealthESB.Domain.IRepository
{
    public interface IDapperRepository
    {
        #region Synchronize
        T ExecuteStoredProcedure<T>(string spName);

        T ExecuteStoredProcedure<T>(string spName, DynamicParameters dynamicParameters);

        List<T> ExecuteStoredProcedureMany<T>(string spName);

        List<T> ExecuteStoredProcedureMany<T>(string spName, DynamicParameters dynamicParameters);

        T ExecuteQuery<T>(string query);

        List<T> ExecuteQueryMany<T>(string query);
        #endregion

        #region Asynchronous
        Task<T> ExecuteStoredProcedureAsync<T>(string spName);

        Task<T> ExecuteStoredProcedureAsync<T>(string spName, DynamicParameters dynamicParameters);

        Task<List<T>> ExecuteStoredProcedureManyAsync<T>(string spName);

        Task<List<T>> ExecuteStoredProcedureManyAsync<T>(string spName, DynamicParameters dynamicParameters);

        //Task<BaseMultipleResult<T, Y>> ExecuteStoredProcedureMultipleAsync<T, Y>(string spName);

        //Task<BaseMultipleResult<T, Y>> ExecuteStoredProcedureMultipleAsync<T, Y>(string spName, DynamicParameters dynamicParameters);

        Task<T> ExecuteQueryAsync<T>(string query);

        Task<List<T>> ExecuteQueryManyAsync<T>(string query);

        Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);

        Task<T> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure);
        #endregion
    }
}
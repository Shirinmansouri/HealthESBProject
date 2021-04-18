using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HealthESB.Domain.IRepository;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HealthESB.Persistance.Repository
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration _config;
        public string ConnectionString { get; set; }

        public DapperRepository(IConfiguration config)
        {
            _config = config;
            ConnectionString =
                config.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        #region Synchronize

        public T ExecuteStoredProcedure<T>(string spName)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(spName, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public T ExecuteStoredProcedure<T>(string spName, DynamicParameters dynamicParameters)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public List<T> ExecuteStoredProcedureMany<T>(string spName)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(spName, commandType: CommandType.StoredProcedure)
                    .ToList();
                sqlConnection.Close();
            }
            return result;
        }

        public List<T> ExecuteStoredProcedureMany<T>(string spName, DynamicParameters dynamicParameters)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure)
                    .ToList();
                sqlConnection.Close();
            }
            return result;
        }

        public T ExecuteQuery<T>(string query)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(query, commandType: CommandType.Text)
                    .FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public List<T> ExecuteQueryMany<T>(string query)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = sqlConnection.
                    Query<T>(query, commandType: CommandType.Text)
                    .ToList();
                sqlConnection.Close();
            }
            return result;
        }

        #endregion

        #region Asynchronous

        public async Task<T> ExecuteStoredProcedureAsync<T>(string spName)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result =
                    (await sqlConnection.QueryAsync<T>(spName, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public async Task<T> ExecuteStoredProcedureAsync<T>(string spName, DynamicParameters dynamicParameters)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = (await sqlConnection.
                    QueryAsync<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure)).FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public async Task<List<T>> ExecuteStoredProcedureManyAsync<T>(string spName)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = (await sqlConnection.
                    QueryAsync<T>(spName, commandType: CommandType.StoredProcedure)).ToList();
                sqlConnection.Close();
            }
            return result;
        }

        public async Task<List<T>> ExecuteStoredProcedureManyAsync<T>(string spName, DynamicParameters dynamicParameters)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = (await sqlConnection.
                    QueryAsync<T>(spName, dynamicParameters, commandType: CommandType.StoredProcedure)).ToList();
                sqlConnection.Close();
            }
            return result;
        }

        //public async Task<BaseMultipleResult<T, Y>> ExecuteStoredProcedureMultipleAsync<T, Y>(string spName)
        //{
        //    var response = new BaseMultipleResult<T, Y>();
        //    using (var sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        sqlConnection.Open();
        //        var result = (await sqlConnection.
        //            QueryMultipleAsync(spName, commandType: CommandType.StoredProcedure));
        //        response.ListOfTData = await result.ReadAsync<T>();
        //        response.ListOfYData = await result.ReadAsync<Y>();
        //        sqlConnection.Close();
        //    }
        //    return response;
        //}

        //public async Task<BaseMultipleResult<T, Y>> ExecuteStoredProcedureMultipleAsync<T, Y>(string spName, DynamicParameters dynamicParameters)
        //{
        //    var response = new BaseMultipleResult<T, Y>();
        //    using (var sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        sqlConnection.Open();
        //        var result = (await sqlConnection.
        //            QueryMultipleAsync(spName, dynamicParameters, commandType: CommandType.StoredProcedure));
        //        response.ListOfTData = await result.ReadAsync<T>();
        //        response.ListOfYData = await result.ReadAsync<Y>();
        //        sqlConnection.Close();
        //    }
        //    return response;
        //}

        public async Task<T> ExecuteQueryAsync<T>(string query)
        {
            T result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result =
                    (await sqlConnection.QueryAsync<T>(query, commandType: CommandType.Text)).FirstOrDefault();
                sqlConnection.Close();
            }
            return result;
        }

        public async Task<List<T>> ExecuteQueryManyAsync<T>(string query)
        {
            List<T> result;
            using (var sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                result = (await sqlConnection.
                    QueryAsync<T>(query, commandType: CommandType.Text)).ToList();
                sqlConnection.Close();
            }
            return result;
        }


        public async Task<T> Insert<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using var tran = sqlConnection.BeginTransaction();
                try
                {
                    result =(await sqlConnection.
                        QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return result;
        }


        public async Task<T> Update<T>(string sp, DynamicParameters parms, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection sqlConnection = new SqlConnection(ConnectionString);
            try
            {
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();

                using var tran = sqlConnection.BeginTransaction();
                try
                {
                    result =(await sqlConnection.
                        QueryAsync<T>(sp, parms, commandType: commandType, transaction: tran)).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                    sqlConnection.Close();
            }

            return result;
        }
        #endregion

    }
}
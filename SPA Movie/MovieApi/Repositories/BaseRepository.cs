using Dapper;
using Microsoft.Data.SqlClient;
using MovieApi.Domains.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApi.Repositories
{
    public class BaseRepository<T> where T : class
    {
      
        private readonly SqlConnection _connectionObject;
        public BaseRepository(string connectionString)
        {
            _connectionObject = new SqlConnection(connectionString);
        }
        public async Task<IEnumerable> GetLists(string query,object sample)
        {
            try
            {
                return await _connectionObject.QueryAsync(query,param: sample);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(string query)
        {
            try
            {
                return await _connectionObject.QueryAsync<T>(query);
            }
            catch(SqlException ex)
            {
               throw new Exception(ex.Message);
            } 
        }
        public async Task<T> GetByIdAsync(string query,object id)
        {
            try
            {
                return await _connectionObject.QueryFirstOrDefaultAsync<T>(query,new { Id=id});
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> GetIdAsync(string query)
        {
            try
            {
                var value = await _connectionObject.ExecuteScalarAsync(query);
                var result = Convert.ToInt32(value);
                return result;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> AddAsync(string query,object sample)
        {
            try
            {
                 return await _connectionObject.QueryFirstOrDefaultAsync<int>(query, param: sample);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(string query,object id)
        {
            try
            {
                await _connectionObject.ExecuteAsync(query,new { Id=id});
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(string query,object sample)
        {
            try
            {
                await _connectionObject.ExecuteAsync(query,param: sample);
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task check(string query,int ids)
        {
            try
            {
                
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        
    }
}

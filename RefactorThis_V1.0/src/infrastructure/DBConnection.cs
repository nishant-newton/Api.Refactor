using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xero.Common.Infrastructure.Interface;
using Xero.Common.Infrastructure.Models;
using DataRow = Xero.Common.Infrastructure.Models.DataRow;

namespace Xero.Common.Infrastructure
{
    public sealed class DBConnection : IDBConnection
    {       

        private SqliteCommand command;
        private SqliteConnection connection;
        private SqliteTransaction transaction;
        private ConnectionState state;
        public DBConnection(string connectionString)
        {
            connection = new SqliteConnection(connectionString);            
        }

        private void Open()
        {
            if(connection.State != ConnectionState.Open)
            {
                state = ConnectionState.Open;
                connection.Open();
            }
        }

        public async Task<object> ExecuteScalar(DataRequest request)
        {
            CreateCommand(request);
            return await command.ExecuteScalarAsync();
        }

        public async Task<int> ExecuteNonQueryAsync(DataRequest request)
        {
            CreateCommand(request);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<DataResponse> GetDataResponseAsync(DataRequest request)
        {
            var response = new DataResponse();
            using (var reader = await ExecuteReaderAsync(request))
            {
                while (reader.Read())
                {
                    var rowList = new DataRow();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var col = new Column();
                        col.ColumnName = reader.GetName(i);
                        col.ColumnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        col.ColumnDataType = reader.GetDataTypeName(i);                        
                        rowList.Columns.Add(col);
                    }
                    response.Rows.Add(rowList);
                }
            }               
            
            return response;
        }

        private async Task<IDataReader> ExecuteReaderAsync(DataRequest request)
        {
            CreateCommand(request);
            return await command.ExecuteReaderAsync();
        }

        private void CreateCommand(DataRequest request)
        {
            EnsureConnectionOpen();
            command = null;          
            
            command = connection.CreateCommand();
            command.CommandText = request.Command;
            if (request.CommandType != null && request.CommandType != CommandType.Text)
            {
                command.CommandType = request.CommandType.Value;
            }
            
            AddParameters(request.Parameters);
        }
        private void AddParameters(List<DataParameter> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var param in parameters)
            {
                var sqlParam = new SqliteParameter();                
                
                if (!string.IsNullOrEmpty(param.ParameterName)) sqlParam.ParameterName = param.ParameterName;
                
                if (param.Value != null) sqlParam.Value = param.Value;                
                
                command.Parameters.Add(sqlParam);
            }
        }

        public void BeginTransaction()
        {
            EnsureConnectionOpen();
            transaction = connection.BeginTransaction();
        }

        public void Commit()
        {
            if (transaction != null)
                transaction.Commit();
        }

        
        public void Rollback()
        {
            if (transaction != null)
                transaction.Rollback();
        }

        private void EnsureConnectionOpen()
        {
            if (state != ConnectionState.Open)
                Open();
        }
    }
}

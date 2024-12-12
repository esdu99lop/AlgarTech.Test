using AlgarTech.Test.Data.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgarTech.Test.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;

        public UnitOfWork(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DataTable> ExecuteStoredProcedureAsync(string storedProcName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();

                try
                {
                    await connection.OpenAsync();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing stored procedure {storedProcName}: {ex.Message}", ex);
                }

                return dataTable;
            }
        }

        public async Task ExecuteNonQueryAsync(string storedProcName, SqlParameter[] parameters = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(storedProcName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                try
                {
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error executing stored procedure {storedProcName}: {ex.Message}", ex);
                }
            }
        }
    }
}

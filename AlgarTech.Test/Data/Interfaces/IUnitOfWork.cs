using Microsoft.Data.SqlClient;
using System.Data;

namespace AlgarTech.Test.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<DataTable> ExecuteStoredProcedureAsync(string storedProcName, SqlParameter[] parameters = null);
        Task ExecuteNonQueryAsync(string storedProcName, SqlParameter[] parameters = null);
    }
}

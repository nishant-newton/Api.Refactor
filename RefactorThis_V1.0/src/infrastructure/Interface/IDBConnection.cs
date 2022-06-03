using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xero.Common.Infrastructure.Models;

namespace Xero.Common.Infrastructure.Interface
{
    public interface IDBConnection
    {
        Task<int> ExecuteNonQueryAsync(DataRequest request);

        Task<DataResponse> GetDataResponseAsync(DataRequest request);

        Task<object> ExecuteScalar(DataRequest request);

        void BeginTransaction();

        void Commit();
        void Rollback();
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Repository.Repositories
{
    public interface IDatabaseFactory : IDisposable
    {
        void Create();
        IDbTransaction Transaction { get; set; }
        IDbConnection Connection { get; set; }
        IDbCommand CreateCommand(bool useTransaction);
    }
}

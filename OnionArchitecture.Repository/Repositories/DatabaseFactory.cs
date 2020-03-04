using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Repository.Repositories
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private bool _hasConnection;
        public IDbTransaction Transaction { get; set; }
        public IDbConnection Connection { get; set; }

        public DatabaseFactory()
        {
            Create();
        }

        public void Create()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; //setup connectionstring

            var connection = new SqlConnection(connString);

            connection.Open();

            Connection = connection;

            _hasConnection = true;
        }

        public IDbCommand CreateCommand(bool useTransaction = true)
        {
            var command = Connection.CreateCommand();
            if (useTransaction)
                Transaction = Transaction ?? Connection.BeginTransaction();
            else
                Transaction = null;
            command.Transaction = Transaction;
            return command;
        }

        protected override void DisposeCore()
        {
            if (Transaction != null)
            {
                Transaction.Rollback();
                Transaction = null;
            }

            if (Connection != null && _hasConnection)
            {
                Connection.Close();
                Connection = null;
            }
        }
    }
}

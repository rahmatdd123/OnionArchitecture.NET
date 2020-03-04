using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnionArchitecture.Core.Interfaces.DomainServices.Repositories;

namespace OnionArchitecture.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly IDatabaseFactory databaseFactory;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
            //UseTransaction();
        }

        public void Commit()
        {
            if (databaseFactory.Transaction == null)
            {
                throw new InvalidOperationException(
                    "SQL transaction is null");
            }

            databaseFactory.Transaction.Commit();
            databaseFactory.Transaction = null;
        }
    }
}

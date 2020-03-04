using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Interfaces.DomainServices.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}

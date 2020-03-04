using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Interfaces.ApplicationServices
{
    public interface ITodoService
    {
        IEnumerable<Todo> GetTodos();

    }
}

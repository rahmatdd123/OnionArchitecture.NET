using OnionArchitecture.Core.Interfaces.ApplicationServices;
using OnionArchitecture.Core.Interfaces.DomainServices.Repositories;
using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Core.Services
{
    public class TodoService : ITodoService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly ITodoRepository todoRepository;

        public TodoService(ITodoRepository todoRepository,
                              IUnitOfWork unitOfWork)
        {
            this.todoRepository = todoRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Todo> GetTodos()
        {
            return todoRepository.GetTodos();
        }
    }
}

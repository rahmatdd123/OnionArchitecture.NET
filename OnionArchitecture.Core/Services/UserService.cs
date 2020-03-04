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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository,
                              IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetUsers(User user)
        {
            IEnumerable<User> users = userRepository.GetUsers();
            var result = from x in users
                                  where x.Username == user.Username && x.Password == user.Password
                                  select x;
            return result;
        }

        public string Register(User user)
        {
            string result = userRepository.Register(user);
            unitOfWork.Commit();
            return result;
        }
    }
}

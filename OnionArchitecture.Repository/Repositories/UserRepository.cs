using OnionArchitecture.Core.Interfaces.DomainServices.Repositories;
using OnionArchitecture.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseFactory _databaseFactory;

        public UserRepository(IDatabaseFactory databaseFactory)
        {
            if (databaseFactory == null)
                throw new ArgumentNullException("DatabaseFactory is null");

            _databaseFactory = databaseFactory as DatabaseFactory;

            if (_databaseFactory == null)
            {
                throw new NotSupportedException("DatabaseFactory is null");
            }
        }

        public IEnumerable<User> GetUsers()
        {
            using (var cmd = _databaseFactory.CreateCommand())
            {
                //SearchBorrowing searchBorrowing = new SearchBorrowing();
                SqlParameter[] parameters = new SqlParameter[0];

                using (var reader = SqlHelper.GetDataReader((SqlConnection)cmd.Connection, "USP_SelectAllUser", (SqlCommand)cmd, CommandType.StoredProcedure, parameters.Cast<SqlParameter>().ToList()))
                {
                    return MappingUsersList(reader).ToList();
                }
            }
        }

        public string Register(User user)
        {
            string result = "";
            using (var cmd = _databaseFactory.CreateCommand())
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter() { Value = user.Username, ParameterName = "@UserName" };
                parameters[1] = new SqlParameter() { Value = user.Password, ParameterName = "@Password" };

                var db = SqlHelper.DoSqlAggregateQuery((SqlConnection)cmd.Connection, CommandType.StoredProcedure, (SqlCommand)cmd, "USP_RegisterUser", parameters.Cast<SqlParameter>().ToList(), (SqlTransaction)cmd.Transaction);
                result = db.ToString();
            }
            return result;
        }

        private IEnumerable<User> MappingUsersList(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    User users = new User();

                    users.Id = Convert.ToInt32(reader["Id"]);
                    users.Username = reader["Username"].ToString();
                    users.Password = reader["Password"].ToString();

                    yield return users;
                }
            }
        }
    }
}

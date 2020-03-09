using OnionArchitecture.Core.Interfaces.DomainServices.Repositories;
using OnionArchitecture.Core.Models;
using OnionArchitecture.Repository.ADO.NET;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionArchitecture.Repository.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private DatabaseFactory _databaseFactory;

        public TodoRepository(IDatabaseFactory databaseFactory)
        {
            if (databaseFactory == null)
                throw new ArgumentNullException("DatabaseFactory is null");

            _databaseFactory = databaseFactory as DatabaseFactory;

            if (_databaseFactory == null)
            {
                throw new NotSupportedException("DatabaseFactory is null");
            }
        }

        public IEnumerable<Todo> GetTodos()
        {
            using (var cmd = _databaseFactory.CreateCommand())
            {
                //SearchBorrowing searchBorrowing = new SearchBorrowing();
                SqlParameter[] parameters = new SqlParameter[0];

                using (var reader = SqlHelper.GetDataReader((SqlConnection)cmd.Connection, "USP_GetAllTask", (SqlCommand)cmd, CommandType.StoredProcedure, parameters.Cast<SqlParameter>().ToList()))
                {
                    return MappingTodoList(reader).ToList();
                }
            }
            //var test = from abc in db.Tbl_User
            //           select new Todo
            //           {
            //               Id = abc.Id,
            //               TaskName = abc.UserName
            //           };
            //return test.ToList();

        }
        private IEnumerable<Todo> MappingTodoList(IDataReader reader)
        {
            using (reader)
            {
                while (reader.Read())
                {
                    Todo todo = new Todo();

                    todo.Id = Convert.ToInt32(reader["Id"]);
                    todo.TaskName = reader["TaskName"].ToString();
                    todo.TaskDetails = reader["TaskDetails"].ToString();
                    todo.TaskDate = reader["TaskDate"].ToString();
                    todo.TaskStatus = reader["TaskStatus"].ToString();

                    yield return todo;
                }
            }
        }
    }
}

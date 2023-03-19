using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.DataAccess;
using StudentManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Todos
{
    public class TodoService : ITodoReposiroty
    {
        private readonly StudentManagementDbContext _context = new StudentManagementDbContext();

        public Todo CreateTodo(int studentId, Todo newTodo)
        {
            var idParam = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var titleparam = new SqlParameter("@Title", newTodo.Title);
            var descriptionparam = new SqlParameter("@Description", newTodo.Description);
            var createdparam = new SqlParameter("@Created", newTodo.Created);
            var endparam = new SqlParameter("@End", newTodo.End);
            var statusparam = new SqlParameter("@Status", newTodo.Status);
            var studentIdparam = new SqlParameter("@studentId", studentId);

            _context.Database.ExecuteSqlRaw("CreateTodo @Id OUTPUT, @Title, @Description, @Created, @End, @Status,@studentId",
                idParam, titleparam, descriptionparam, createdparam, endparam, statusparam, studentIdparam);
            int addedTodoId = (int)idParam.Value;
            return _context.Todo.Find(addedTodoId);

        }

        public void DeleteTodo(int todoId)
        {
            var todoIdParam = new SqlParameter("@Id", todoId);
            _context.Database.ExecuteSqlRaw("DeleteTodo @Id", todoIdParam);
        }

        public ICollection<Todo> GetAllTodos()
        {
            return _context.Todo.FromSqlRaw($"EXEC GetTodos").ToList();
        }

        public ICollection<Todo> GetAllTodos(int studentId)
        {
            var iDParam = new SqlParameter("@StudentId", studentId);
            return _context.Todo.FromSqlRaw($"EXEC GetTodos @StudentId", iDParam).ToList();

        }

        public Todo GetTodo(int todoId)
        {
            var idParam = new SqlParameter("@TodoId", todoId);
            return _context.Todo.FromSqlRaw("EXEC GetTodoFromId @TodoId", idParam).AsEnumerable().FirstOrDefault();
        }

        public Todo GetTodo(int studentId, int todoId)
        {
            var studentIdParam = new SqlParameter("@StudentId", studentId);
            var todoIdParam = new SqlParameter("@TodoId", todoId);
            return _context.Todo.FromSqlRaw($"EXEC GetTodos @StudentId,@TodoId", studentIdParam, todoIdParam).AsEnumerable().FirstOrDefault();
        }

        public void UpdateTodo(Todo updateTodo)
        {
            var idParam = new SqlParameter("@Id", updateTodo.Id) ;
            var titleparam = new SqlParameter("@Title", updateTodo.Title);
            var descriptionparam = new SqlParameter("@Description", updateTodo.Description);
            var createdparam = new SqlParameter("@Created", updateTodo.Created);
            var endparam = new SqlParameter("@End", updateTodo.End);
            var statusparam = new SqlParameter("@Status", updateTodo.Status);
            var studentIdparam = new SqlParameter("@studentId", updateTodo.StudentId);

            _context.Database.ExecuteSqlRaw("UpdateTodo @Id, @Title, @Description, @Created, @End, @Status,@studentId",
                idParam, titleparam, descriptionparam, createdparam, endparam, statusparam, studentIdparam);

        }
    }
}

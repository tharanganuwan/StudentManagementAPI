using StudentManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Todos
{
    public interface ITodoReposiroty
    {
        public Todo CreateTodo(int studentId,Todo newTodo);
        public ICollection<Todo> GetAllTodos();
        public ICollection<Todo> GetAllTodos(int studentId);
        public Todo GetTodo(int todoId);
        public Todo GetTodo(int studentId, int todoId);
        public void DeleteTodo(int todoId);
        public void UpdateTodo(Todo updateTodo);
    }
}

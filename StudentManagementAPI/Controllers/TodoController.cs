using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using StudentManagementAPI.Services.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementAPI.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoReposiroty _service;
        private readonly IMapper _mapper;

        public TodoController(ITodoReposiroty service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpPost("{studentId}")]
        public ActionResult<TodoDto> CreateTodo(int studentId,CreateTodoDto todo) 
        {
            var createTodoEntity = _mapper.Map<Todo>(todo);
            var addedTodo = _service.CreateTodo(studentId, createTodoEntity);
            var returnTodo = _mapper.Map<TodoDto>(addedTodo);
            return Ok(returnTodo);
        }
        [HttpGet]
        public ActionResult<TodoDto> GetAllTodo() 
        {
            List<Todo> todos = _service.GetAllTodos().ToList();
            if (todos is null) return NoContent();
            var todoDtos = _mapper.Map<ICollection<TodoDto>>(todos);

            return Ok(todoDtos);
        }

        
        [HttpGet("student/{studentId}")]
        public ActionResult<TodoDto> GetAllTodos(int studentId)
        {
            List<Todo> todos = _service.GetAllTodos(studentId).ToList();
            if (todos is null) return NoContent();
            var todoDtos = _mapper.Map<ICollection<TodoDto>>(todos);

            return Ok(todoDtos);
        }

        [HttpGet("{todoId}")]
        public ActionResult<TodoDto> GetTodo(int todoId)
        {
            var todo = _service.GetTodo(todoId);
            if (todo is null) return NoContent();
            var todoDto = _mapper.Map<TodoDto>(todo);

            return Ok(todoDto);
        }

        [HttpDelete("{todoId}")]
        public IActionResult DeleteTodo(int todoId)
        {
            var deleteTodo = _service.GetTodo(todoId);
            if (deleteTodo is null) return NoContent();
            _service.DeleteTodo(todoId);
            return Ok();
        }

        [HttpPut("{todoId}")]
        public IActionResult UpdateTodo(int todoId,UpdateTodoDto todo)
        {
            var updateTodo = _service.GetTodo(todoId);
            if (updateTodo is null) return NoContent();
 

            var updatedTodo= _mapper.Map<Todo>(todo);
            updatedTodo.Id = todoId;
            _service.UpdateTodo(updatedTodo);

            return Ok();
        }
    }
}

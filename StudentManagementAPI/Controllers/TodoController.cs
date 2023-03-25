using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
            try
            {
                var createTodoEntity = _mapper.Map<Todo>(todo);
                var addedTodo = _service.CreateTodo(studentId, createTodoEntity);
                var returnTodo = _mapper.Map<TodoDto>(addedTodo);
                return Ok(returnTodo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Creating new todo: {ex.Message}");
            }
            
        }
        [HttpGet]
        public ActionResult<TodoDto> GetAllTodo() 
        {
            try
            {
                List<Todo> todos = _service.GetAllTodos().ToList();
                if (todos is null) return NoContent();
                var todoDtos = _mapper.Map<ICollection<TodoDto>>(todos);

                return Ok(todoDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Todos: {ex.Message}");

            }    
        }

        
        [HttpGet("student/{studentId}")]
        public ActionResult<TodoDto> GetAllTodos(int studentId)
        {
            try
            {
                List<Todo> todos = _service.GetAllTodos(studentId).ToList();
                if (todos is null) return NoContent();
                var todoDtos = _mapper.Map<ICollection<TodoDto>>(todos);

                return Ok(todoDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Todo: {ex.Message}");
            }
            
        }

        [HttpGet("{todoId}")]
        public ActionResult<TodoDto> GetTodo(int todoId)
        {
            try
            {
                var todo = _service.GetTodo(todoId);
                if (todo is null) return NoContent();
                var todoDto = _mapper.Map<TodoDto>(todo);

                return Ok(todoDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving Todo: {ex.Message}");
            }
        }

        [HttpDelete("{todoId}")]
        public IActionResult DeleteTodo(int todoId)
        {
            try
            {
                var deleteTodo = _service.GetTodo(todoId);
                if (deleteTodo is null) return NoContent();
                _service.DeleteTodo(todoId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Deleting Todo: {ex.Message}");
            }
            
        }

        [HttpPut("{todoId}")]
        public IActionResult UpdateTodo(int todoId,UpdateTodoDto todo)
        {
            try
            {
                var updateTodo = _service.GetTodo(todoId);
                if (updateTodo is null) return NoContent();
                var updatedTodo = _mapper.Map<Todo>(todo);
                updatedTodo.Id = todoId;
                _service.UpdateTodo(updatedTodo);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while Updadating Todo: {ex.Message}");
            }
            
        }
    }
}

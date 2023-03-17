using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using StudentManagementAPI.Services.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentReposiroty _service;
        private readonly IMapper _mapper;

        public StudentController(IStudentReposiroty service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult CreateStudent(CreateStudentDto student) 
        {
            var studentEntity = _mapper.Map<Student>(student);
            var createdStudent =  _service.CreateStudent(studentEntity);

            return Ok(createdStudent);
        }

        [HttpGet]
        public IActionResult GetAllStudents() {
            List<Student> students = _service.GetAllStudents();
            if (students is null) return NoContent();

            return Ok(students);
        }

        [HttpGet("{studentId}")]
        public IActionResult GetAllStudents(int studentId)
        {
            Student student = _service.GetStudent(studentId);
            if (student is null) return NotFound();
            return Ok(student);
        }

        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId) 
        {
            var deleteStudent = _service.GetStudent(studentId);
            if (deleteStudent is null)
            {
                return NoContent();
            }
            _service.DeleteStudent(studentId);

            return Ok();
        }

        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, CreateStudentDto studentDto)
        {
            var updateStudent = _service.GetStudent(studentId);
            if (updateStudent is null)
            {
                return NoContent();
            }
            
            var student = _mapper.Map<Student>(studentDto);
            student.Id = studentId;
            _service.UpdateStudent(student);

            return Ok();
        }


    }
}

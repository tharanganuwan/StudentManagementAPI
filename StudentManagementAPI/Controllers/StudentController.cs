using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using StudentManagementAPI.Helpers;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using StudentManagementAPI.Services.Students;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentReposiroty _service;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentReposiroty service, IMapper mapper, ILogger<StudentController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("authenticateJWT")]
        public IActionResult Authenticate() 
        {
            var claims = new[]
             {
                new Claim("FullName","Tharanga Nuwan"),
                new Claim(JwtRegisteredClaimNames.Sub,"user_id")
             };

            var keyBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                Constants.Audience,
                Constants.Issure,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { accessToken = tokenString });
        }



        [Authorize]
        [HttpPost]
        public ActionResult<StudentDto> CreateStudent(CreateStudentDto student) 
        {
            var studentEntity = _mapper.Map<Student>(student);
            var createdStudent =  _service.CreateStudent(studentEntity);
            var returnStudent = _mapper.Map<StudentDto>(createdStudent);
            _logger.LogInformation("add student : "+returnStudent);
            return Ok(returnStudent);
        }

        [HttpGet]
        public ActionResult<StudentDto> GetAllStudents() {
            List<Student> students = _service.GetAllStudents();
            if (students is null) return NoContent();

            var returnStudents = _mapper.Map<ICollection<StudentDto>>(students);

            return Ok(returnStudents);
        }

        [HttpGet("{studentId}")]
        public IActionResult GetAllStudents(int studentId)
        {
            Student student = _service.GetStudent(studentId);
            if (student is null) return NotFound();
            var returnStudent = _mapper.Map<StudentDto>(student);
            return Ok(returnStudent);
        }

        [Authorize]
        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId) 
        {
            var deleteStudent = _service.GetStudent(studentId);
            if (deleteStudent is null)
            {
                return NoContent();
            }
            _service.DeleteStudent(studentId);
            _logger.LogInformation("Deleted student Id : " + studentId);
            return Ok();
        }

        [Authorize]
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

        [HttpGet("search")]
        public ActionResult<ICollection<StudentDto>> SearchStudentFromName([FromQuery] String name)
        {
            List<Student> students = _service.SerchFromName(name);
            if (students is null) return NoContent();
            var studentDtos = _mapper.Map<ICollection<StudentDto>>(students);
            return Ok(studentDtos);
        }

    }
}

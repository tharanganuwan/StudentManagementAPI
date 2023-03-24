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
            try
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
                _logger.LogInformation($"Created  accessToken in Authenticate method");
                return Ok(new { accessToken = tokenString });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Creating Token: {ex.Message}");
                return StatusCode(500, $"An error occurred while Creating Token: {ex.Message}");
            }
            
        }



        [Authorize]
        [HttpPost]
        public ActionResult<StudentDto> CreateStudent(CreateStudentDto student) 
        {
            try
            {
                var studentEntity = _mapper.Map<Student>(student);
                var createdStudent = _service.CreateStudent(studentEntity);
                var returnStudent = _mapper.Map<StudentDto>(createdStudent);
                _logger.LogInformation($"Save studentId: {returnStudent.Id} in CreateStudent method");
                return Ok(returnStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Save New student : {ex.Message}");
                return StatusCode(500, $"An error occurred while Save New student : {ex.Message}");
            }

            
        }

        [HttpGet]
        public ActionResult<StudentDto> GetAllStudents() {
            try
            {
                List<Student> students = _service.GetAllStudents();
                if (students is null)
                {
                    _logger.LogInformation("No students found in GetAllStudents method");
                    return NoContent();
                } 
                var returnStudents = _mapper.Map<ICollection<StudentDto>>(students);
                _logger.LogInformation($"Retrieved {students.Count} students in GetAllStudents method");
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving students in GetAllStudents method : {ex.Message}");
                return StatusCode(500, $"An error occurred while retrieving students: {ex.Message}");
            }
        }

        [HttpGet("{studentId}")]
        public IActionResult GetAllStudents(int studentId)
        {
            try
            {
                Student student = _service.GetStudent(studentId);
                if (student is null)
                {
                    _logger.LogInformation("No student found in GetAllStudents method");
                    return NotFound();
                }
                var returnStudent = _mapper.Map<StudentDto>(student);
                _logger.LogInformation($"Retrieved studentId : {returnStudent.Id} in GetAllStudents method");
                return Ok(returnStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving student in GetAllStudents method: {ex.Message}");
                return StatusCode(500, $"An error occurred while retrieving student: {ex.Message}");
            }
            
        }

        [Authorize]
        [HttpDelete("{studentId}")]
        public IActionResult DeleteStudent(int studentId) 
        {
            try
            {
                var deleteStudent = _service.GetStudent(studentId);
                if (deleteStudent is null)
                {
                    _logger.LogInformation("No student found in DeleteStudent method");
                    return NoContent();
                }
                _service.DeleteStudent(studentId);
                _logger.LogInformation($"Delete StudentId : {studentId} in DeleteStudent method");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Deleting student: {ex.Message}");
                return StatusCode(500, $"An error occurred while Deleting student in DeleteStudent method: {ex.Message}");
            }

            
        }

        [Authorize]
        [HttpPut("{studentId}")]
        public IActionResult UpdateStudent(int studentId, CreateStudentDto studentDto)
        {
            try
            {
                var updateStudent = _service.GetStudent(studentId);
                if (updateStudent is null)
                {
                    _logger.LogInformation("No student found in UpdateStudent method");
                    return NoContent();
                }

                var student = _mapper.Map<Student>(studentDto);
                student.Id = studentId;
                _service.UpdateStudent(student);
                _logger.LogInformation($"Update StudentId : {student.Id} in UpdateStudent method");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Updating student in UpdateStudent method : {ex.Message}");
                return StatusCode(500, $"An error occurred while Updating student: {ex.Message}");
            }
            
        }

        [HttpGet("search")]
        public ActionResult<ICollection<StudentDto>> SearchStudentFromName([FromQuery] String name)
        {
            try
            {
                List<Student> students = _service.SerchFromName(name);
                if (students is null) return NoContent();
                var studentDtos = _mapper.Map<ICollection<StudentDto>>(students);
                _logger.LogInformation($"Retrieved {studentDtos.Count} students in SearchStudentFromName method");
                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while Searching student in SearchStudentFromName method : {ex.Message}");
                return StatusCode(500, $"An error occurred while Searching student: {ex.Message}");
            }
            
        }

    }
}

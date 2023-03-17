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

namespace StudentManagementAPI.Services.Students
{
    public class StudentService : IStudentReposiroty
    {
        private readonly StudentManagementDbContext _context = new StudentManagementDbContext();
        public Student CreateStudent(Student student)
        {
            var idParam = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var firstNameParam = new SqlParameter("@FirstName", student.FirstName);
            var middleNameParam = new SqlParameter("@MiddleName", student.MiddleName);
            var lastNameParam = new SqlParameter("@LastName", student.LastName);
            var dobParam = new SqlParameter("@Dob", student.Dob);
            var motherNameParam = new SqlParameter("@MotherName", student.MotherName);
            var fatherNameParam = new SqlParameter("@FatherName", student.FatherName);

            _context.Database.ExecuteSqlRaw("CreateStudent @Id OUTPUT, @FirstName, @MiddleName, @LastName, @Dob, @MotherName,@FatherName",
                idParam,firstNameParam,middleNameParam,lastNameParam,dobParam,motherNameParam,fatherNameParam);
            int addedStudentId = (int)idParam.Value;
            return _context.Student.Find(addedStudentId);

        }

        public Student GetStudent(int id)
        {
            var studentId = new SqlParameter("@Id", id);
            return _context.Student.FromSqlRaw($"GetStudentFromId @Id",studentId).AsEnumerable().FirstOrDefault();
        }

        public List<Student> GetAllStudents()
        {
            return _context.Student.FromSqlRaw($"GetAllStudents").ToList();
        }

        public void DeleteStudent(int studentId)
        {
            var employeeIdParam = new SqlParameter("@Id", studentId);
            _context.Database.ExecuteSqlRaw("DeleteStudent @Id", employeeIdParam);
        }

        public void UpdateStudent(Student student)
        {
            var idParam = new SqlParameter("@Id", student.Id);
            var firstNameParam = new SqlParameter("@FirstName", student.FirstName);
            var middleNameParam = new SqlParameter("@MiddleName", student.MiddleName);
            var lastNameParam = new SqlParameter("@LastName", student.LastName);
            var dobParam = new SqlParameter("@Dob", student.Dob);
            var motherNameParam = new SqlParameter("@MotherName", student.MotherName);
            var fatherNameParam = new SqlParameter("@FatherName", student.FatherName);

            _context.Database.ExecuteSqlRaw("UpdateStudent @Id, @FirstName, @MiddleName, @LastName, @Dob, @MotherName,@FatherName",
                idParam, firstNameParam, middleNameParam, lastNameParam, dobParam, motherNameParam, fatherNameParam);
            
            
        }
    }
}

using StudentManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Students
{
    public interface IStudentReposiroty
    {
        public Student GetStudent(int id);
        public List<Student> GetAllStudents();
        public Student CreateStudent(Student newStudents);
        public void DeleteStudent(int studentId);
        public void UpdateStudent(Student student);
        public List<Student> SerchFromName(String name);
    }
}

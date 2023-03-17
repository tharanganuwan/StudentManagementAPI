using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.DataAccess
{
    public class StudentManagementDbContext : DbContext
    {
        public DbSet<Student> Student { get; set; }
        public DbSet<Todo> Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost; Database=Student_Db; User Id=sa; password=5503;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentManagementAPI.DataAccess;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.users
{
    public class UserService : IUserRepository
    {
        private readonly StudentManagementDbContext _context = new StudentManagementDbContext();

        public User Authenticate(UserLoginDto user)
        {
            var emailParam = new SqlParameter("@Email", user.Email);
            var passwordParam = new SqlParameter("@Password", user.Password);
            return _context.User.FromSqlRaw("EXEC Authenticate @Email, @Password", emailParam,passwordParam).AsEnumerable().FirstOrDefault();
        }

        public User CreateUser(User user)
        {
            var idParam = new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output };
            var usernameParm = new SqlParameter("@UserName", user.UserName);
            var emailParm = new SqlParameter("@Email", user.Email);
            var paswordParm = new SqlParameter("@Password", user.Password);
            var statusParm = new SqlParameter("@Status", 1);

            _context.Database.ExecuteSqlRaw($"EXEC CreateUser @Id OUTPUT,@UserName, @Email,@Password, @Status",
               idParam, usernameParm, emailParm, paswordParm, statusParm);
            int addedUserId = (int)idParam.Value;
            return _context.User.Find(addedUserId);
        }

        public List<User> GetAllUser()
        {
            
             List<User> users = _context.User.FromSqlRaw("EXEC GetUsers").ToList();
             return users;
        }

        public User GetUser(int id)
        {
            var idParam = new SqlParameter("@Id", id);
            return _context.User.FromSqlRaw("EXEC GetUsers @Id", idParam).AsEnumerable().FirstOrDefault();
        }

        public User GetUserFromEmail(string email)
        {
            var emailParam = new SqlParameter("@Email", email);
            return _context.User.FromSqlRaw("EXEC GetUserFromEmail @Email", emailParam).AsEnumerable().FirstOrDefault();
        }

        public void UpdateUser(User user)
        {
            var idParam = new SqlParameter("@Id", user.Id);
            var usernameParm = new SqlParameter("@UserName", user.UserName);
            var emailParm = new SqlParameter("@Email", user.Email);
            var paswordParm = new SqlParameter("@Password", user.Password);
            var statusParm = new SqlParameter("@Status", user.Status);

            _context.Database.ExecuteSqlRaw($"EXEC UpdateUser @Id, @UserName, @Email,@Password, @Status",
               idParam, usernameParm, emailParm, paswordParm, statusParm);
        }
    }
}

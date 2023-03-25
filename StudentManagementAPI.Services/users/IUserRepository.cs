using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.users
{
    public interface IUserRepository
    {
        public User GetUserFromEmail(string email);
        public User CreateUser(User user);
        public void UpdateUser(User user);
        public User GetUser(int id);
        public List<User> GetAllUser();
        public User Authenticate(UserLoginDto user);
    }
}

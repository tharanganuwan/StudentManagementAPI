using AutoMapper;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<UpdateUserDto, User>();
        }
    }
}

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
    public class TodoProfile : Profile
    {
        public TodoProfile()
        {
            CreateMap<CreateTodoDto, Todo>();
            CreateMap<Todo, TodoDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Enum.GetName(typeof(TodoStatus), src.Status)));
            CreateMap<UpdateTodoDto, Todo>();
        }
    }
}

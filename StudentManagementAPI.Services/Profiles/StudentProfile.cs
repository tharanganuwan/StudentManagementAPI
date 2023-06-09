﻿using AutoMapper;
using StudentManagementAPI.Models;
using StudentManagementAPI.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Profiles
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<CreateStudentDto, Student>();
            CreateMap<Student, StudentDto>()
                .ForMember(name => name.Name, opt => opt
                .MapFrom(src => $"{src.FirstName} {src.MiddleName} {src.LastName}"));
        }
    }
}


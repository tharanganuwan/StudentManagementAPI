using StudentManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Dtos
{
    public class UpdateTodoDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime End { get; set; }

        public TodoStatus Status { get; set; }

        public int StudentId { get; set; }
    }
}

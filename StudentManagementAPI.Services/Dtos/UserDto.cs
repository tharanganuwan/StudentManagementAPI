using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public int Status { get; set; }
    }
}

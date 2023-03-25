using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementAPI.Services.Dtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public string password { get; set; }
    }
}

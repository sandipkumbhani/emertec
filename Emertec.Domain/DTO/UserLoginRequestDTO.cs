using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class UserLoginRequestDTO
    {
        public string? EmailId { get; set; }
        public string? Password { get; set; }

    }
}

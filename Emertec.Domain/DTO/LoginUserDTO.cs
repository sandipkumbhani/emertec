using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class LoginUserDTO
    {

        public long UserId { get; set; }
        public string? EmailId { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string Token { get; set; } = string.Empty;


    }
}

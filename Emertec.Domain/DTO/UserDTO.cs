using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class UserDTO
    {
      
            public long UserId { get; set; }
            public string? Name { get; set; }
            public string? EmailId { get; set; }
            public int UserRoleId { get; set; }
            public bool IsActive { get; set; }
        

    }
}

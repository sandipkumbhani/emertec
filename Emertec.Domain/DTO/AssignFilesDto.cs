using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class AssignFilesDto
    {
       public int UserId { get; set; }
       public List<Guid>? JsonIds { get; set; }
        

    }
}

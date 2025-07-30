using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Domain.AssignFiles
{
    public class AssignFilesDto
    {
        public int UserId { get; set; }
        public List<Guid> jsonid { get; set; } = new List<Guid>();
    }
}

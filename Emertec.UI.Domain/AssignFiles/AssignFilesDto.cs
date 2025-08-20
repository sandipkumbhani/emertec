using MicroService_Template.Domain.Model;
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
            public List<string>? SelectedFileNames { get; set; }
            public List<Guid>? JsonIds { get; set; }

            public IList<ModelUsers>? UserList { get; set; }
            public IList<string>? FileList { get; set; }
            public long loggedInUserId { get; set; }





    }
}

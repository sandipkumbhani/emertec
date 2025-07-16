using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class MenuMasterDTO
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Icon { get; set; }
    }
}

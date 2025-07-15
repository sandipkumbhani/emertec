using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Model
{
    public class ModelUserRole
    {
        [Key]
        public int UserRoleId { get; set; }
        [StringLength(200)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }

        public long InsertBy { get; set; }

        public DateTime InsertDate { get; set; }

        public long UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}

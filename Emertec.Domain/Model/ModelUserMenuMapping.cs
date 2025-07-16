using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Model
{
    public class ModelUserMenuMapping
    {
        [Key]
       public long UserMenuMappingId { get; set; }
       
        public long UserId { get; set; }

        public long MenuId { get; set; }
   
        public bool IsActive { get; set; }

        public long InsertBy { get; set; }

        public DateTime InsertDate { get; set; }

        public long UpdateBy { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("UserId")]
        public virtual ModelUsers User { get; set; }
        

        [ForeignKey("MenuId")]
        public virtual ModelMenuMaster Menu { get; set; }
    }
}

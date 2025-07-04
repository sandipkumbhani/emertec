using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Model
{
    public class ModelUserLogin 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        
        public string? Salt { get; set; }
    }
}

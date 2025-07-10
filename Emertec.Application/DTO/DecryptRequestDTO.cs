using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.DTO
{
    public class DecryptRequestDTO
    {
        public string BasePath { get; set; }
        public string PrivateKeyPath { get; set; }
        public string whisperExePath { get; set; }

        public string guidPath { get; set; }
    }
}

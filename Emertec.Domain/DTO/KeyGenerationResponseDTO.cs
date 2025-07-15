using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.DTO
{
    public class KeyGenerationResponseDTO
    {
        public string PrivateKeyPath { get; set; }
        public string PublicKeyPath { get; set; }
        public string Message { get; set; }
    }
}

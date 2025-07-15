using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Services
{
    public class CryptoService : ICryptoService
    {
        public KeyGenerationResponseDTO GenerateRsaKeys(string outputFolder)
        {
            Directory.CreateDirectory(outputFolder);

            using RSA rsa = RSA.Create(2048);

            string privateKeyXml = rsa.ToXmlString(true);
            string publicKeyXml = rsa.ToXmlString(false);

            string privateKeyPath = Path.Combine(outputFolder, "private_key.xml");
            string publicKeyPath = Path.Combine(outputFolder, "public_key.xml");

            File.WriteAllText(privateKeyPath, privateKeyXml);
            File.WriteAllText(publicKeyPath, publicKeyXml);

            return new KeyGenerationResponseDTO
            {
                PrivateKeyPath = privateKeyPath,
                PublicKeyPath = publicKeyPath,
                Message = "RSA key pair generated successfully."
            };
        }
    }
}

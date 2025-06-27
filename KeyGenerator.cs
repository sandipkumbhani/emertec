using System;
using System.IO;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        using RSA rsa = RSA.Create(2048);

        // Save private key (used for decryption)
        string privateKey = rsa.ToXmlString(true); // true = includes private part
        Directory.CreateDirectory(@"D:\Keys");
        File.WriteAllText(@"D:\Keys\private_key.xml", privateKey);

        // Save public key (used for encryption)
        string publicKey = rsa.ToXmlString(false); // false = public part only
        File.WriteAllText(@"D:\Keys\public_key.xml", publicKey);

        Console.WriteLine("✅ Keys created successfully.");
    }
}
using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace MicroService_Template.Application.Services
{
    public class AudioFileService : IAudioFileService
    {
        private readonly MP3Settings _mp3Settings;

        public AudioFileService(IOptions<MP3Settings> mp3Settings)
        {
            _mp3Settings = mp3Settings.Value;
        }
        public List<string> ConvertAllMp3FilesToRsaAndGuid(AudioPaths _paths)
        {
            var rsaFiles = new List<string>();

            if (!Directory.Exists(_paths.BasePath))
                throw new DirectoryNotFoundException($"Base path not found: {_paths.BasePath}");

            if (string.IsNullOrWhiteSpace(_paths.PublicKeyPath) || !File.Exists(_paths.PublicKeyPath))
                throw new FileNotFoundException("Public key not found at path: " + _paths.PublicKeyPath);
            using var rsa = RSA.Create();
            string publicKeyXml = File.ReadAllText(_paths.PublicKeyPath);
            rsa.FromXmlString(publicKeyXml);

            // Get all subfolders inside the base directory
            var subFolders = Directory.GetDirectories(_paths.BasePath);
            var datePattern = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            foreach (var folder in subFolders)
            {
                string currentSource = folder;
                if (!Directory.Exists(currentSource))
                    continue;

                string folderName = Path.GetFileName(currentSource);
                if (!datePattern.IsMatch(folderName))
                    continue;

                string rsaFolder = Path.Combine(_paths.BasePath, folderName + "-RSA");
                string guidBaseFolder = Path.Combine(_paths.BasePath, folderName + "-GUID");

                Directory.CreateDirectory(rsaFolder);
                Directory.CreateDirectory(guidBaseFolder);

                string[] allFiles = Directory.GetFiles(currentSource, "*.mp3");
                if (allFiles.Length == 0) continue;

                foreach (string mp3File in allFiles)
                {
                    byte[] mp3Data = File.ReadAllBytes(mp3File);
                    byte[] toEncrypt = mp3Data.Take(200).ToArray(); // Limit to 200 bytes for 2048-bit RSA
                    byte[] rest = mp3Data.Skip(200).ToArray();

                    byte[] encryptedData = rsa.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);
                    byte[] combined = encryptedData.Concat(rest).ToArray();

                    string fileName = Path.GetFileNameWithoutExtension(mp3File);
                    string rsaFilePath = Path.Combine(rsaFolder, $"{fileName}.rsa");
                    File.WriteAllBytes(rsaFilePath, combined);
                    rsaFiles.Add(rsaFilePath);

                    string guidFilePath = Path.Combine(guidBaseFolder, $"{fileName}.guid");

                    if (!File.Exists(guidFilePath))
                    {
                        string guid = Guid.NewGuid().ToString();
                        File.WriteAllText(guidFilePath, guid);
                        Console.WriteLine($"GUID saved to: {guidFilePath}");
                    }
                    else
                    {
                        Console.WriteLine($"GUID file already exists. Skipping creation: {guidFilePath}");
                    }

                    if (_mp3Settings.IsdeleteMp3File)
                    {
                        File.Delete(mp3File);
                    }
                }

                try
                {
                    if (Directory.Exists(currentSource))
                    {
                        Directory.Delete(currentSource, recursive: false);
                        Console.WriteLine($"Force deleted folder: {currentSource}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete folder {currentSource}: {ex.Message}");
                }
            }

            return rsaFiles;
        }





    }
}




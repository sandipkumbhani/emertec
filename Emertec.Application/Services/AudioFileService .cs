using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Interface;
using Microsoft.Extensions.Options;
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

                if (allFiles.Length == 0)
                    continue;

                using var rsa = RSA.Create(2048);


                foreach (string mp3File in allFiles)
                {
                    // Read the MP3 data
                    byte[] mp3Data = File.ReadAllBytes(mp3File);
                    byte[] toEncrypt = mp3Data.Take(200).ToArray();
                    byte[] rest = mp3Data.Skip(200).ToArray();
                    byte[] encryptedData = rsa.Encrypt(toEncrypt, RSAEncryptionPadding.Pkcs1);
                    byte[] combined = encryptedData.Concat(rest).ToArray();

                    string fileName = Path.GetFileNameWithoutExtension(mp3File);
                    string rsaFilePath = Path.Combine(rsaFolder, $"{fileName}.rsa");
                    File.WriteAllBytes(rsaFilePath, combined);
                    rsaFiles.Add(rsaFilePath);

                    // Create a GUID folder if needed
                    string guid = Guid.NewGuid().ToString();
                    string guidFolder = Path.Combine(guidBaseFolder, guid);
                    if (!Directory.Exists(guidFolder))
                        Directory.CreateDirectory(guidFolder);
                    else
                    {
                        Console.WriteLine($"Directory already exists: {guidFolder}");
                    }
                    //Delete the original MP3 file
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


        public List<string> ConvertAllRsaFilesToJson(AudioPaths _paths)
        {
            var resultFiles = new List<string>();

            if (!Directory.Exists(_paths.BasePath))
                throw new DirectoryNotFoundException($"Base path not found: {_paths.BasePath}");

            // Get all subfolders inside the base directory
            var subFolders = Directory.GetDirectories(_paths.BasePath);

            foreach (var folder in subFolders)
            {
                string folderName = Path.GetFileName(folder);

                // Check for folders ending with -RSA or -GUID
                if (folderName.EndsWith("-RSA", StringComparison.OrdinalIgnoreCase))
                {
                    // Get all .rsa files in this folder
                    var rsaFiles = Directory.GetFiles(folder, "*.rsa", SearchOption.AllDirectories);
                    resultFiles.AddRange(rsaFiles);
                }
                if (folderName.EndsWith("-GUID", StringComparison.OrdinalIgnoreCase))
                {
                    var allFiles = Directory.GetFiles(folder, "*", SearchOption.AllDirectories);

                    var guidFiles = allFiles.Where(file =>
                    {
                        string name = Path.GetFileName(file);
                        return Guid.TryParse(name, out _); // Check if filename is a valid GUID
                    }).ToList();

                    resultFiles.AddRange(guidFiles);
                } 
               
            }

            return resultFiles;
        }


    }
}




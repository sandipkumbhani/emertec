using MicroService_Template.Application.DTO;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using MicroService_Template.Domain.Model;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;

namespace MicroService_Template.Application.Services
{

    public class ConvertRsaToJsonService : IConvertRsaToJsonService
    {
        private readonly MP3Settings _mp3Settings;

        private readonly IModelDimJsonRepository _modelDimJsonRepository;


        public ConvertRsaToJsonService(IOptions<MP3Settings> mp3Settings, IModelDimJsonRepository repository)
        {
            _mp3Settings = mp3Settings.Value;
            _modelDimJsonRepository = repository;
        }


        public async Task<List<string>> WorkerMp3ToJson(DecryptRequest request, string privateKeyPath, string whisperExePath)
        {
            var mp3Files = new List<string>();
            var jsonFiles = new List<string>();
            //call getallrsa file method
            var rsaFiles = GetAllRsaFiles(request);
            foreach (var rsaFilePath in rsaFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(rsaFilePath);
                string rsaFolder = Path.GetDirectoryName(rsaFilePath)!;
                string mp3Folder = rsaFolder.Replace("-RSA", "-MP3");


                Directory.CreateDirectory(mp3Folder);

                string mp3Path = Path.Combine(mp3Folder, fileName + ".mp3");

                string jsonFolder = rsaFolder.Replace("-RSA", "-JSON");
                Directory.CreateDirectory(jsonFolder);
                string jsonPath = Path.Combine(jsonFolder, fileName + ".json");

               


                /*if (File.Exists(mp3Path))
                {
                    Console.WriteLine($"MP3 already exists. Skipping: {mp3Path}");
                    continue;
                }*/

                //call decrypt method 
                /*DecryptRsaToMp3(rsaFilePath, privateKeyPath, mp3Path);*/
                if (!File.Exists(mp3Path))
                {
                    DecryptRsaToMp3(rsaFilePath, privateKeyPath, mp3Path);
                    Console.WriteLine($"MP3 created: {mp3Path}");
                }
                else
                {
                    Console.WriteLine($"MP3 already exists: {mp3Path}");
                }

                mp3Files.Add(mp3Path);
                /*//call mp3 to json method
                ConvertMp3ToJson(mp3Path, jsonFolder, whisperExePath);
                // Skip if JSON already exists
                if (File.Exists(jsonPath))
                {
                    Console.WriteLine($"JSON already exists. Skipping: {jsonPath}");
                    jsonFiles.Add(jsonPath);
                    continue;
                }*/
                if (!File.Exists(jsonPath))
                {
                    ConvertMp3ToJson(mp3Path, jsonFolder, whisperExePath);
                    Console.WriteLine($"JSON created: {jsonPath}");
                }
                else
                {
                    Console.WriteLine($"JSON already exists: {jsonPath}");
                    jsonFiles.Add(jsonPath);
                    continue; 
                }

                string guidFolder = rsaFolder.Replace("-RSA", "-GUID");
                string guidPath = Path.Combine(guidFolder, fileName + ".guid");

                if (!File.Exists(guidPath))
                {
                    Console.WriteLine($"GUID file not found for: {fileName}");
                    continue; // or handle error
                }

                AppendGuidToJson(jsonPath, guidPath);

                await SaveFileAndGeneratedGuidAsync(guidPath);

                if (_mp3Settings.IsdeleteRsaFiles)
                {
                    File.Delete(rsaFilePath);
                }
            }

            return mp3Files;
        }
        //get all rsafolder 

        private List<string> GetAllRsaFiles(DecryptRequest _request)
        {
            var rsaFiles = new List<string>();

            if (!Directory.Exists(_request.BasePath))
                throw new DirectoryNotFoundException($"Base path not found: {_request.BasePath}");

            // Get all subfolders inside the base directory
            var subFolders = Directory.GetDirectories(_request.BasePath);
            var rsaDateFolderPattern = new Regex(@"^\d{4}-\d{2}-\d{2}-RSA$");

            foreach (var folder in subFolders)
            {
                string currentSource = folder;
                if (!Directory.Exists(currentSource))
                    continue;

                string folderName = Path.GetFileName(currentSource);
                if (!rsaDateFolderPattern.IsMatch(folderName))
                    continue;

                foreach (var folder1 in folderName)
                {
                    string rsafile = Path.GetFileName(folderName);


                    string[] files = Directory.GetFiles(folder, "*.rsa", SearchOption.AllDirectories);
                    rsaFiles.AddRange(files);
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

        // Decrypt method to convert rsa to mp3
        private void DecryptRsaToMp3(string rsaFilePath, string privateKeyPath, string outputMp3Path)
        {
            byte[] encryptedFile = File.ReadAllBytes(rsaFilePath);

            if (new FileInfo(rsaFilePath).Length < 256)
            {
                Console.WriteLine($"Skipping file (too small): {rsaFilePath}");
                return;
            }
            byte[] encryptedHeader = encryptedFile.Take(256).ToArray();
            byte[] mp3Rest = encryptedFile.Skip(256).ToArray();

            string privateKeyXml = File.ReadAllText(privateKeyPath);
            using RSA rsa = RSA.Create();
            rsa.FromXmlString(privateKeyXml);
            byte[] decryptedHeader = rsa.Decrypt(encryptedHeader, RSAEncryptionPadding.Pkcs1);
            byte[] fullMp3 = decryptedHeader.Concat(mp3Rest).ToArray();

            // Save output MP3
            File.WriteAllBytes(outputMp3Path, fullMp3);
            Console.WriteLine($" Decryption successful! MP3 saved at: {outputMp3Path}");
        }
        private void ConvertMp3ToJson(string mp3Path, string jsonOutputFolder, string whisperExePath)
        {
            Directory.CreateDirectory(jsonOutputFolder);

            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(mp3Path);
            string expectedJsonPath = Path.Combine(jsonOutputFolder, fileNameWithoutExt + ".json");

            if (File.Exists(expectedJsonPath))
            {
                Console.WriteLine($"JSON already exists. Skipping: {expectedJsonPath}");
                return;
            }

            var arguments = new List<string>
    {
        "--model medium",
        "--compute_type float32",
        "--threads 4",
        "--output_format json",
        "--word_timestamps true",
        "--task translate",
        $"-o \"{jsonOutputFolder}\"",
        $"\"{mp3Path}\""
    };

            var startInfo = new ProcessStartInfo
            {
                FileName = whisperExePath,
                Arguments = string.Join(" ", arguments),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };

            // Capture real-time output
            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[stdout] " + e.Data);
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine("[stderr] " + e.Data);
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Whisper failed.");
            }
            else
            {
                Console.WriteLine($"Generated JSON: {expectedJsonPath}");
            }
        }

        private void AppendGuidToJson(string jsonPath, string guidPath)
        {
            if (!File.Exists(jsonPath))
                throw new FileNotFoundException("JSON file not found.");

            if (!File.Exists(guidPath))
                throw new FileNotFoundException("GUID file not found.");
            if (Path.GetFullPath(jsonPath).Equals(Path.GetFullPath(guidPath), StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JSON file and GUID file cannot be the same.");
            string guid = File.ReadAllText(guidPath).Trim();
            string json = File.ReadAllText(jsonPath);
            var transcript = JsonConvert.DeserializeObject<VoiceFileExtendedJson>(json);
            transcript.Guid = guid;
            string fileName = Path.GetFileNameWithoutExtension(jsonPath);
            var metadata = ParseFileName(fileName);

            transcript.AgentUsername = metadata.AgentUsername;
            transcript.AgentFirstName = metadata.AgentFirstName;
            transcript.AgentLastName = metadata.AgentLastName;
            transcript.AgentSalutation = metadata.AgentSalutation;
            transcript.CampaignName = metadata.CampaignName;
            transcript.CampaignDate = metadata.CampaignDate;
            transcript.CallNumber = metadata.CallNumber;
            transcript.CallDateTime = metadata.CallDateTime;
            transcript.TeamName = metadata.TeamName;
            transcript.CallRespondentFullPath = metadata.CallRespondentFullPath;
            if (transcript.Segments != null)
            {
                foreach (var segment in transcript.Segments)
                {
                    segment.text = segment.text?.Trim();
                    if (segment.words != null)
                    {
                        foreach (var word in segment.words)
                        {
                            word.word = word.word?.Trim();
                        }
                    }
                }
            }
            string updatedJson = JsonConvert.SerializeObject(transcript, Formatting.Indented);
            File.WriteAllText(jsonPath, updatedJson);
        }

        private VoiceFileExtendedJson ParseFileName(string fileName)
        {
            var parts = fileName.Split('_');
            var result = new VoiceFileExtendedJson();

            // Extract AgentUsername, FirstName, LastName
            if (parts[0].StartsWith("AN"))
            {
                string fullName = parts[0].Substring(2);
                result.AgentUsername = fullName;

                var nameParts = Regex.Split(fullName, @"(?=[A-Z])")
                       .Where(p => !string.IsNullOrWhiteSpace(p))
                       .ToArray();

                if (nameParts.Length >= 2)
                {
                    result.AgentFirstName = nameParts[0];
                    result.AgentLastName = string.Join("", nameParts.Skip(1));
                }
                else
                {
                    result.AgentFirstName = fullName;
                    result.AgentLastName = "";
                }
            }
            string ciRaw = parts.FirstOrDefault(p => p.StartsWith("CI"))?.Substring(2);
            if (!string.IsNullOrEmpty(ciRaw))
            {
                result.CampaignName = ciRaw;
            }


            string dtRaw = parts.FirstOrDefault(p => p.StartsWith("DT"))?.Substring(2);
            if (!string.IsNullOrEmpty(dtRaw) && dtRaw.Length >= 14)
            {
                result.CampaignDate = DateTime.ParseExact(dtRaw.Substring(0, 8), "ddMMyyyy", null).ToString("yyyy-MM-dd");
            }

            string utcstRaw = parts.FirstOrDefault(p => p.StartsWith("UTCST"))?.Substring(5);
            if (!string.IsNullOrEmpty(utcstRaw) && utcstRaw.Length >= 14)
            {
                result.CallNumber = utcstRaw.Substring(0, 14);
                result.CallDateTime = DateTime.ParseExact(result.CallNumber, "yyyyMMddHHmmss", null)
                    .ToString("yyyy-MM-ddTHH:mm:ss");
            }

            return result;
        }
        private async Task<string> SaveFileAndGeneratedGuidAsync(string guidPath)
        {
            try
            {
                if (!File.Exists(guidPath))
                    throw new FileNotFoundException("GUID file not found.", guidPath);

                string fileGuidText = await File.ReadAllTextAsync(guidPath);

                if (!Guid.TryParse(fileGuidText.Trim(), out Guid fileGuidValue))
                    throw new FormatException("The file does not contain a valid GUID.");
                var existing = await _modelDimJsonRepository.GetByDapperGuidAsync(fileGuidValue);
                if (existing != null)
                {
                    Console.WriteLine($"Record with GUID {fileGuidValue} already exists. Skipping insert.");
                    return $"Duplicate record skipped for GUID: {fileGuidValue}";
                }

                var model = new ModelDimJson
                {
                    Id = Guid.NewGuid(),
                    DapperGuid = fileGuidValue,
                    CampaignId = Guid.Empty,
                    FileName = null,
                    FilePath = null,
                    LastSyncDateTime = DateTime.UtcNow,
                    IsExecuted = false,
                    IsRepeat = false,
                    CallLength = 0,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                };

                await _modelDimJsonRepository.InsertJsonRecordAsync(model);

                return "ModelDimJson record saved successfully.";
            }
            catch (Exception ex)
            {

                return $"Error: {ex.Message}";
            }
        }

    }

}




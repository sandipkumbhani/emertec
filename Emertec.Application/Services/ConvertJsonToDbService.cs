using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroService_Template.Domain.Services
{
    public class ConvertJsonToDbService : IConvertJsonToDbService
    {
        private readonly IModelDimJsonRepository _modelDimJsonRepository;
        private readonly IModelDimAgentRepository _modelDimAgentRepository;
        private readonly IModelDimCampaignRepository _modelDimCampaignRepository;
        private readonly IModelDimCompanyRepository _modelDimCompanyRepository;
        private readonly IModelDimTextFullRepository _modelDimTextFullRepository;
        private readonly IModelDimTextSentenceRepository _modelDimTextSentenceRepository;
        private readonly IModelDimTextWordRepositorycs _modelDimTextWordRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
      
        public ConvertJsonToDbService(IModelDimJsonRepository modelDimJsonRepository, IModelDimAgentRepository modelDimAgentRepository,
            IModelDimCampaignRepository modelDimCampaignRepository, IModelDimCompanyRepository modelDimCompanyRepository,
            IModelDimTextFullRepository modelDimTextFullRepository, IModelDimTextSentenceRepository modelDimTextSentenceRepository, IModelDimTextWordRepositorycs modelDimTextWordRepositorycs, 
            IHttpContextAccessor httpContextAccessor)
        {
            _modelDimJsonRepository = modelDimJsonRepository;
            _modelDimAgentRepository = modelDimAgentRepository;
            _modelDimCampaignRepository = modelDimCampaignRepository;
            _modelDimCompanyRepository = modelDimCompanyRepository;
            _modelDimTextFullRepository = modelDimTextFullRepository;
            _modelDimTextSentenceRepository = modelDimTextSentenceRepository;
            _modelDimTextWordRepository = modelDimTextWordRepositorycs;
            _httpContextAccessor = httpContextAccessor;
          
        }
        private List<string> GetALLJsonFiles(JsonToDbDTO _jsontodb)
        {
            var rsaFiles = new List<string>();

            if (!Directory.Exists(_jsontodb.BasePath))
            {
                throw new DirectoryNotFoundException($"Base path not found: {_jsontodb.BasePath}");
            }
            // Get all subfolders inside the base directory
            var subFolders = Directory.GetDirectories(_jsontodb.BasePath);
            var rsaDateFolderPattern = new Regex(@"^\d{4}-\d{2}-\d{2}-JSON$");

            foreach (var folder in subFolders)
            {
                string folderName = Path.GetFileName(folder);
                if (!rsaDateFolderPattern.IsMatch(folderName))
                    continue;
                string[] files = Directory.GetFiles(folder, "*.json", SearchOption.AllDirectories);
                rsaFiles.AddRange(files);

            }
            return rsaFiles;
        }
        public async Task<List<ModelDimJson>> SaveJsonToDB(JsonToDbDTO jsonToDb)
        {
            var saveRecord = new List<ModelDimJson>();
            var jsonFiles = GetALLJsonFiles(jsonToDb);
            var processedAgents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var processedCampaigns = new HashSet<string>();

            foreach (var file in jsonFiles)
            {
                if (!File.Exists(file)) continue;

                var json = await File.ReadAllTextAsync(file);
                var data = JsonConvert.DeserializeObject<VoiceFileExtendedJson>(json);
                var fileName = Path.GetFileName(file);

                bool fileNameExists = await _modelDimJsonRepository.ExistsByFileNameAsync(fileName);
                if (fileNameExists)
                {
                    continue;
                }
                var addrecord = new ModelDimJson
                {
                    Id = Guid.NewGuid(),
                    FileName = Path.GetFileName(file),
                    FilePath = file,
                    TelephoneNumber = data?.TelephoneNumber?.Trim(),
                    UserId = 0,
                    IsActive = true,
                    InsertBy = 1, 
                    InsertDate = DateTime.Now,
                    UpdateBy = 1,
                    UpdateDate = DateTime.Now,
                    
                   
                };
                await _modelDimJsonRepository.InsertJsonRecordAsync(addrecord);
                await _modelDimJsonRepository.SaveChangesAsync();

                saveRecord.Add(addrecord);

                string agentKey = $"{data.AgentFirstName?.Trim()}|{data.AgentLastName?.Trim()}";

                if (!processedAgents.Contains(agentKey))
                {
                    processedAgents.Add(agentKey);

                    bool agentExists = await _modelDimAgentRepository.AgentExistsAsync(data.AgentFirstName, data.AgentLastName);
                    if (!agentExists)
                    {
                        var agent = new ModelDimAgent
                        {
                            Id = Guid.NewGuid(),
                            FirstName = data.AgentFirstName,
                            LastName = data.AgentLastName,
                            Created = DateTime.Now,
                            Modified = DateTime.Now
                        };

                        await _modelDimAgentRepository.InsertAgentAsync(agent);
                    }
                }
                if (!string.IsNullOrWhiteSpace(data.CampaignName))
                {
                    var CompanyName = data.CampaignName.Trim();
                    if (!processedCampaigns.Contains(CompanyName))
                    {
                        processedCampaigns.Add(CompanyName);

                        var existingCampaign = await _modelDimCompanyRepository.GetByNameAsync(CompanyName);
                        if (existingCampaign == null)
                        {
                            var company = new ModelDimCompany
                            {
                                Id = Guid.NewGuid(),
                                Name = CompanyName,
                                Description = "null",
                                Created = DateTime.Now,
                                Modified = DateTime.Now
                            };

                            await _modelDimCompanyRepository.companyInsertAsync(company);
                            await _modelDimCompanyRepository.SaveChangesAsync();
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(data.CampaignDate) && !string.IsNullOrWhiteSpace(data.CampaignName))
                {
                    var campaignDate = data.CampaignDate.Trim();
                    var campaignPath = Path.GetDirectoryName(file);
                    var campaignName = data.CampaignName.Trim();

                    if (!processedCampaigns.Contains(campaignDate))
                    {
                        processedCampaigns.Add(campaignDate);

                        var existingCampaign = await _modelDimCampaignRepository.GetByNameAsync(campaignDate);
                        var existingCompany = await _modelDimCompanyRepository.GetCompanyIdByNameAsync(campaignName);
                        var companyId = existingCompany?.Id;

                        if (existingCampaign == null)
                        {
                            var campaign = new ModelDimCampaign
                            {
                                Id = Guid.NewGuid(),
                                CompanyId = companyId,
                                Name = campaignDate,
                                Campaignpath = campaignPath,
                                Created = DateTime.Now,
                                Modified = DateTime.Now
                            };

                            await _modelDimCampaignRepository.campaignInsertAsync(campaign);
                            await _modelDimCampaignRepository.SaveChangesAsync();
                        }
                    }
                }
                var allWords = data.Segments?
                    .SelectMany(seg => seg.words)
                    .Where(w => !string.IsNullOrWhiteSpace(w.word))
                    .Select(w => w.word.Trim())
                    .ToList();

                string fullText = string.Join(" ", allWords ?? new List<string>());

                var textFull = new ModelDimTextFull
                {
                    Id = Guid.NewGuid(),
                    JsonGuid = null,
                    name = $"{data.AgentFirstName} {data.AgentLastName}".Trim(),
                    campaign_date = data.CampaignDate,
                    FullText = fullText,
                    Size = allWords?.Count.ToString(),
                    Created = DateTime.Now,
                    Modified = DateTime.Now
                };
                await _modelDimTextFullRepository.InsertAsync(textFull);
                await _modelDimTextFullRepository.SaveChangesAsync();

                foreach (var segment in data.Segments)
                {
                    var sentence = new ModelDimTextSentence
                    {
                        Id = Guid.NewGuid(),
                        //JsonGuid = null,
                        Sentence = segment.text?.Trim(),
                        //StartTime = baseTime.AddSeconds(segment.start),
                        //EndTime = baseTime.AddSeconds(segment.end),
                        Speaker = segment.Speaker,
                        Created = DateTime.UtcNow,
                        Modified = DateTime.UtcNow
                    };

                    await _modelDimTextSentenceRepository.InsertAsync(sentence);
                    await _modelDimTextSentenceRepository.SaveChangesAsync();

                    if (segment.words != null)
                    {
                        foreach (var word in segment.words)
                        {
                            var wordEntity = new ModelDimWord
                            {
                                Id = Guid.NewGuid(),
                                //JsonId = 123,
                                TextSentenceId = sentence.Id,
                                Word = word.word,
                                OriginalWord = null,
                                SoundsLike = null,
                                Fuzzy = null,
                                RateProfanity = 0,
                                RateComplexity = 0,
                                VoicePrint = null,
                                StartTime = word.start,
                                EndTime = word.end,
                                Speaker = segment.Speaker,
                                Probability = double.TryParse(word.probability, out var prob) ? prob : 0,
                                Created = DateTime.UtcNow,
                                Modified = DateTime.UtcNow
                            };

                            await _modelDimTextWordRepository.InsertAsync(wordEntity);
                            await _modelDimTextWordRepository.SaveChangesAsync();

                        }
                    }
                }
            }
            return saveRecord;

        }
    }
}



//public async Task<List<ModelDimJson>> SaveJsonToDB(JsonToDbDTO jsonToDb)
//{
//    var updatedRows = new List<ModelDimJson>();
//    var jsonFiles = GetALLJsonFiles(jsonToDb);
//    var processedAgents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
//    var processedCampaigns = new HashSet<string>();

//    foreach (var file in jsonFiles)
//    {
//        if (!File.Exists(file)) continue;

//        var json = await File.ReadAllTextAsync(file);
//        var data = JsonConvert.DeserializeObject<VoiceFileExtendedJson>(json);
//        bool filePathExists = await _modelDimJsonRepository.ExistsByFilePathAsync(file);

//        if (!filePathExists)
//        {
//            var addrecord = new ModelDimJson
//            {
//                Id=Guid.NewGuid(),
//                FileName = Path.GetFileName(file),
//                FilePath = file,
//                TelephoneNumber = data.TelephoneNumber?.Trim(),



//            };
//            await _modelDimJsonRepository.InsertJsonRecordAsync(addrecord);
//            //await _modelDimJsonRepository.SaveChangesAsync();
//        }


//                string agentKey = $"{data.AgentFirstName?.Trim()}|{data.AgentLastName?.Trim()}";

//                if (!processedAgents.Contains(agentKey))
//                {
//                    processedAgents.Add(agentKey);

//                    bool agentExists = await _modelDimAgentRepository.AgentExistsAsync(data.AgentFirstName, data.AgentLastName);
//                    if (!agentExists)
//                    {
//                        var agent = new ModelDimAgent
//                        {
//                            Id = Guid.NewGuid(),
//                            FirstName = data.AgentFirstName,
//                            LastName = data.AgentLastName,
//                            Created = DateTime.Now,
//                            Modified = DateTime.Now
//                        };

//                        await _modelDimAgentRepository.InsertAgentAsync(agent);
//                    }
//                }
//                if (!string.IsNullOrWhiteSpace(data.CampaignName))
//                {
//                    var CompanyName = data.CampaignName.Trim();
//                    if (!processedCampaigns.Contains(CompanyName))
//                    {
//                        processedCampaigns.Add(CompanyName);

//                        var existingCampaign = await _modelDimCompanyRepository.GetByNameAsync(CompanyName);
//                        if (existingCampaign == null)
//                        {
//                            var company = new ModelDimCompany
//                            {
//                                Id = Guid.NewGuid(),
//                                Name = CompanyName,
//                                Description = "null",
//                                Created = DateTime.Now,
//                                Modified = DateTime.Now
//                            };

//                            await _modelDimCompanyRepository.companyInsertAsync(company);
//                            await _modelDimCompanyRepository.SaveChangesAsync();
//                        }
//                    }
//                }
//                if (!string.IsNullOrWhiteSpace(data.CampaignDate) && !string.IsNullOrWhiteSpace(data.CampaignName))
//                {
//                    var campaignDate = data.CampaignDate.Trim();
//                    var campaignPath = Path.GetDirectoryName(file);
//                    var campaignName = data.CampaignName.Trim();

//                    if (!processedCampaigns.Contains(campaignDate))
//                    {
//                        processedCampaigns.Add(campaignDate);

//                        var existingCampaign = await _modelDimCampaignRepository.GetByNameAsync(campaignDate);
//                        var existingCompany = await _modelDimCompanyRepository.GetCompanyIdByNameAsync(campaignName);
//                        var companyId = existingCompany?.Id;

//                        if (existingCampaign == null)
//                        {
//                            var campaign = new ModelDimCampaign
//                            {
//                                Id = Guid.NewGuid(),
//                                CompanyId = companyId,
//                                Name = campaignDate,
//                                Campaignpath = campaignPath,
//                                Created = DateTime.Now,
//                                Modified = DateTime.Now
//                            };

//                            await _modelDimCampaignRepository.campaignInsertAsync(campaign);
//                            await _modelDimCampaignRepository.SaveChangesAsync();
//                        }
//                    }
//                }
//                if (!string.IsNullOrWhiteSpace(data.Guid) && Guid.TryParse(data.Guid, out var jsonguid))
//                {
//                    var jsonRecord = await _modelDimJsonRepository.GetByDapperGuidAsync(jsonguid);

//                    if (jsonRecord != null)
//                    {
//                        var existingTextFull = await _modelDimTextFullRepository.GetByJsonGuidAsync(jsonRecord.Id);
//                        if (existingTextFull == null)
//                        {
//                            var allWords = data.Segments?
//                                .SelectMany(seg => seg.words)
//                                .Where(w => !string.IsNullOrWhiteSpace(w.word))
//                                .Select(w => w.word.Trim())
//                                .ToList();

//                            string fullText = string.Join(" ", allWords ?? new List<string>());

//                            var textFull = new ModelDimTextFull
//                            {
//                                Id = Guid.NewGuid(),
//                                JsonGuid = jsonRecord.Id,
//                                name = $"{data.AgentFirstName} {data.AgentLastName}".Trim(),
//                                campaign_date = data.CampaignDate,
//                                FullText = fullText,
//                                Size = allWords?.Count.ToString(),
//                                Created = DateTime.Now,
//                                Modified = DateTime.Now
//                            };

//                            await _modelDimTextFullRepository.InsertAsync(textFull);
//                            await _modelDimTextFullRepository.SaveChangesAsync();
//                        }

//                    }
//                }

//                if (!string.IsNullOrWhiteSpace(data.Guid) && Guid.TryParse(data.Guid, out var jsonguidforsentence))
//                {
//                    var jsonRecord = await _modelDimJsonRepository.GetByDapperGuidAsync(jsonguidforsentence);

//                    if (jsonRecord != null)
//                    {
//                        bool sentenceExists = await _modelDimTextSentenceRepository.ExistsByJsonGuidAsync(jsonRecord.Id);
//                        if (sentenceExists)
//                        {
//                            Console.WriteLine($"Sentences and words for Guid {data.Guid} already exist.");
//                        }
//                        else
//                        {
//                            /*var baseTime = jsonRecord.Created ?? DateTime.UtcNow;*/
//                            foreach (var segment in data.Segments)
//                            {
//                                var sentence = new ModelDimTextSentence
//                                {
//                                    Id = Guid.NewGuid(),
//                                    JsonGuid = jsonRecord.Id,
//                                    Sentence = segment.text?.Trim(),
//                                    //StartTime = baseTime.AddSeconds(segment.start),
//                                    //EndTime = baseTime.AddSeconds(segment.end),
//                                    Speaker = segment.Speaker,
//                                    Created = DateTime.UtcNow,
//                                    Modified = DateTime.UtcNow
//                                };

//                                await _modelDimTextSentenceRepository.InsertAsync(sentence);
//                                await _modelDimTextSentenceRepository.SaveChangesAsync();

//                                if (segment.words != null)
//                                {
//                                    foreach (var word in segment.words)
//                                    {
//                                        var wordEntity = new ModelDimWord
//                                        {
//                                            Id = Guid.NewGuid(),
//                                            JsonId = jsonRecord.Id,
//                                            TextSentenceId = sentence.Id,
//                                            Word = word.word,
//                                            OriginalWord = null,
//                                            SoundsLike = null,
//                                            Fuzzy = null,
//                                            RateProfanity = 0,
//                                            RateComplexity = 0,
//                                            VoicePrint = null,
//                                            StartTime = word.start,
//                                            EndTime = word.end,
//                                            Speaker = segment.Speaker,
//                                            Probability = double.TryParse(word.probability, out var prob) ? prob : 0,
//                                            Created = DateTime.UtcNow,
//                                            Modified = DateTime.UtcNow
//                                        };

//                                        await _modelDimTextWordRepository.InsertAsync(wordEntity);
//                                        await _modelDimTextWordRepository.SaveChangesAsync();
//                                    }
//                                }
//                            }
//                        }

//                    }
//                }

//            }
//            await _modelDimJsonRepository.SaveChangesAsync();
//            return updatedRows;
//        }

//    }

//}

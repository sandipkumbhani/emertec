using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace MicroService_Template.Application.Services
{
    public class ConvertJsonToDb : IConvertJsonToDbService
    {
        private readonly IModelDimJsonRepository _modelDimJsonRepository;
        private readonly IModelDimAgentRepository _modelDimAgentRepository;
        private readonly IModelDimCampaignRepository _modelDimCampaignRepository;
        private readonly IModelDimCompanyRepository _modelDimCompanyRepository;
        private readonly IModelDimTextFullRepository _modelDimTextFullRepository;
        private readonly IModelDimTextSentenceRepository _modelDimTextSentenceRepository;
        private readonly IModelDimTextWordRepositorycs _modelDimTextWordRepository;
        public ConvertJsonToDb(IModelDimJsonRepository modelDimJsonRepository, IModelDimAgentRepository modelDimAgentRepository,
            IModelDimCampaignRepository modelDimCampaignRepository, IModelDimCompanyRepository modelDimCompanyRepository,
            IModelDimTextFullRepository modelDimTextFullRepository, IModelDimTextSentenceRepository modelDimTextSentenceRepository, IModelDimTextWordRepositorycs modelDimTextWordRepositorycs)
        {
            _modelDimJsonRepository = modelDimJsonRepository;
            _modelDimAgentRepository = modelDimAgentRepository;
            _modelDimCampaignRepository = modelDimCampaignRepository;
            _modelDimCompanyRepository = modelDimCompanyRepository;
            _modelDimTextFullRepository = modelDimTextFullRepository;
            _modelDimTextSentenceRepository = modelDimTextSentenceRepository;
            _modelDimTextWordRepository = modelDimTextWordRepositorycs;
        }

        public List<string> GetALLJsonFiles(JsonToDB _jsontodb)
        {
            var rsaFiles = new List<string>();

            if (!Directory.Exists(_jsontodb.BasePath))
                throw new DirectoryNotFoundException($"Base path not found: {_jsontodb.BasePath}");

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
        public async Task<List<ModelDimJson>> CheckGuidFromJsonAsync(JsonToDB jsonToDb)
        {
            var updatedRows = new List<ModelDimJson>();
            var jsonFiles = GetALLJsonFiles(jsonToDb);
            var processedAgents = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var processedCampaigns = new HashSet<string>();

            foreach (var file in jsonFiles)
            {
                if (!File.Exists(file)) continue;

                var json = await File.ReadAllTextAsync(file);
                var data = JsonConvert.DeserializeObject<VoiceFileExtendedJson>(json);

                if (string.IsNullOrWhiteSpace(data?.Guid) || !Guid.TryParse(data.Guid, out var parsedGuid))
                    continue;

                var existing = await _modelDimJsonRepository.GetByDapperGuidAsync(parsedGuid);

                if (existing != null)
                {
                    existing.FileName = Path.GetFileName(file);
                    existing.FilePath = file;
                    existing.LastSyncDateTime = DateTime.Now;
                    existing.Modified = DateTime.Now;

                    await _modelDimJsonRepository.UpdateAsync(existing);
                    updatedRows.Add(existing);
                }

                // Insert Agent if not exists
                string agentKey = $"{data.AgentFirstName?.Trim()}|{data.AgentLastName?.Trim()}";

                if (!processedAgents.Contains(agentKey))
                {
                    processedAgents.Add(agentKey); // Mark as processed

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

                if (!string.IsNullOrWhiteSpace(data.CampaignDate))
                {
                    var campaignDate = data.CampaignDate.Trim(); // e.g., "2024-07-10"
                    var campaignPath = Path.GetDirectoryName(file);

                    if (!processedCampaigns.Contains(campaignDate))
                    {
                        if (!string.IsNullOrWhiteSpace(data.CompanyId) && Guid.TryParse(data.CompanyId, out var CompanyId))
                        {
                            processedCampaigns.Add(campaignDate);

                            var existingCampaign = await _modelDimCampaignRepository.GetByNameAsync(campaignDate);
                            var dimCompany = await _modelDimCompanyRepository.GetByCompanyIdAsync(CompanyId);


                            if (existingCampaign == null)
                            {
                                var campaign = new ModelDimCampaign
                                {
                                    Id = Guid.NewGuid(),
                                    CompanyId = dimCompany.Id,
                                    Name = campaignDate,
                                    Campaignpath = campaignPath,
                                    Created = DateTime.Now,
                                    Modified = DateTime.Now
                                };

                                await _modelDimCampaignRepository.campaignInsertAsync(campaign);
                            }
                        }
                    }
                }

                
                if (!string.IsNullOrWhiteSpace(data.Guid) && Guid.TryParse(data.Guid, out var jsonguid))
                {

                    var jsonRecord = await _modelDimJsonRepository.GetByDapperGuidAsync(jsonguid);

                    if (jsonRecord != null)
                    {


                        var allWords = data.Segments?
                            .SelectMany(seg => seg.words)
                            .Where(w => !string.IsNullOrWhiteSpace(w.word))
                            .Select(w => w.word.Trim())
                            .ToList();

                        string fullText = string.Join(" ", allWords ?? new List<string>());

                        var textFull = new ModelDimTextFull
                        {
                            Id = Guid.NewGuid(),
                            JsonGuid = jsonRecord.Id,
                            name = $"{data.AgentFirstName} {data.AgentLastName}".Trim(),
                            campaign_date = data.CampaignDate,
                            FullText = fullText,
                            Size = allWords?.Count.ToString(),
                            Created = DateTime.Now,
                            Modified = DateTime.Now
                        };

                        await _modelDimTextFullRepository.InsertAsync(textFull);
                        await _modelDimTextFullRepository.SaveChangesAsync();
                    }
                }

                if (!string.IsNullOrWhiteSpace(data.Guid) && Guid.TryParse(data.Guid, out var jsonguidfoesentence))
                {
                    var jsonRecord = await _modelDimJsonRepository.GetByDapperGuidAsync(jsonguidfoesentence);

                    if (jsonRecord != null)
                    {
                        bool sentenceExists = await _modelDimTextSentenceRepository.ExistsByJsonGuidAsync(jsonRecord.Id);
                        if (sentenceExists)
                        {
                            Console.WriteLine($"Sentences and words for Guid {data.Guid} already exist.");
                        }
                        else
                        {
                            var baseTime = jsonRecord.Created ?? DateTime.UtcNow;
                            foreach (var segment in data.Segments)
                            {
                                // First, insert sentence (optional, if needed for sentence linking)
                                var sentence = new ModelDimTextSentence
                                {
                                    Id = Guid.NewGuid(),
                                    JsonGuid = jsonRecord.Id,
                                    Sentence = segment.text?.Trim(),
                                    StartTime = baseTime.AddSeconds(segment.start),
                                    EndTime = baseTime.AddSeconds(segment.end),
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
                                            JsonId = jsonRecord.Id,
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

                    }
                }

            }
            await _modelDimJsonRepository.SaveChangesAsync();
            return updatedRows;
        }

    }

}

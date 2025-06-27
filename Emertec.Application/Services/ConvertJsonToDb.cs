using MicroService_Template.Application.DTO;
using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroService_Template.Application.Services
{
    public class ConvertJsonToDb : IConvertJsonToDb
    {
        private readonly IModelDimJsonRepository _modelDimJsonRepository;
        private readonly IModelDimAgentRepository _modelDimAgentRepository;
        private readonly IModelDimCampaignRepository _modelDimCampaignRepository;
        private readonly IModelDimCompanyRepository _modelDimCompanyRepository;

        public ConvertJsonToDb(IModelDimJsonRepository modelDimJsonRepository, IModelDimAgentRepository modelDimAgentRepository, IModelDimCampaignRepository modelDimCampaignRepository, IModelDimCompanyRepository modelDimCompanyRepository)
        {
            _modelDimJsonRepository = modelDimJsonRepository;
            _modelDimAgentRepository = modelDimAgentRepository;
            _modelDimCampaignRepository = modelDimCampaignRepository;
            _modelDimCompanyRepository = modelDimCompanyRepository;

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
                        processedCampaigns.Add(campaignDate);

                        var existingCampaign = await _modelDimCampaignRepository.GetByNameAsync(campaignDate);
                        if (existingCampaign == null)
                        {
                            var campaign = new ModelDimCampaign
                            {
                                Id = Guid.NewGuid(),
                                Name = campaignDate,
                                Campaignpath = campaignPath,
                                Created = DateTime.Now,
                                Modified = DateTime.Now
                            };

                            await _modelDimCampaignRepository.campaignInsertAsync(campaign);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(data.CampaignName))
                {
                    var campaignName = data.CampaignName.Trim();



                    if (!processedCampaigns.Contains(campaignName))
                    {
                        processedCampaigns.Add(campaignName);

                        var existingCampaign = await _modelDimCompanyRepository.GetcompanyNameAsync(campaignName);
                        if (existingCampaign == null)
                        {
                            var company = new ModelDimCompany
                            {
                                Id = Guid.NewGuid(),
                                Name = campaignName,
                                Description = "null",
                                Created = DateTime.Now,
                                Modified = DateTime.Now
                            };

                            await _modelDimCompanyRepository.companyInsertAsync(company);
                        }
                    }
                }
            }


            await _modelDimJsonRepository.SaveChangesAsync();
            return updatedRows;
        }


    }
}

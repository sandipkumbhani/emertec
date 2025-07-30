using MicroService_Template.Application.Extension.Interface;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Services
{
    public class ShowTrancriptService : IShowTrancriptService
    {
        private readonly IShowTrancriptRepository _showTrancriptRepository;
        public ShowTrancriptService(IShowTrancriptRepository showTrancriptRepository)
        {
            _showTrancriptRepository = showTrancriptRepository ?? throw new ArgumentNullException(nameof(showTrancriptRepository));
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscript()
        {
            return await _showTrancriptRepository.GetFileNamesByIsTranscriptAsync();
        }
        public async Task<List<string>> GetByFileNameAsync(string filename)
        {

            var jsonRecord = await _showTrancriptRepository.GetByFileNameAsync(filename);

            if (jsonRecord == null)
            {
                return new List<string> { "No record found for this FileName." };
            }

            if (!File.Exists(jsonRecord.FilePath))
            {
                return new List<string> { "File Not Found" };
            }
            try
            {
                var jsonContent = await File.ReadAllTextAsync(jsonRecord.FilePath);

                var voiceJson = JsonConvert.DeserializeObject<VoiceFileExtendedJson>(jsonContent);
                var filteredSegments = voiceJson?.Segments?
                    .Where(seg => seg.Speaker != null && !string.IsNullOrWhiteSpace(seg.text))
                    .Select(seg => $"Speaker {seg.Speaker}: {seg.text.Trim()}")
                    .ToList();

                return filteredSegments ?? new List<string>();
            }

            catch
            {
                return new List<string> { "Error reading or parsing the JSON file." };
            }
        }


    }
}

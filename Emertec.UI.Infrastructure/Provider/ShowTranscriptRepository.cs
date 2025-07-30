using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Emertec.UI.Infrastructure.Provider
{
    public class ShowTranscriptRepository : IShowTranscriptRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public ShowTranscriptRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptedAsync()
        {
            var baseUrl = apiCredential.url + "ShowTrancript/GetFileNameByIsTranscripted";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelDimJson>>(json)!;
        }
        public async Task<ModelDimJson> GetByFileNameAsync(string fileName)
        {
            var baseUrl = apiCredential.url + $"ShowTrancript/sentences-from-FileName?fileName={fileName}";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelDimJson>(json)!;
        }
    }
}

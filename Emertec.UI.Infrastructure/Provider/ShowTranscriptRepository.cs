using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

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
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<ModelDimJson>(); 
            }
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelDimJson>>(json)!;
        }
        public async Task<List<ModelDimJson>> GetFileNameByIsTranscriptTrueAsync()
        {
            var baseUrl = apiCredential.url + "ShowTrancript/GetFileNameByIsTranscriptTrue";
            var response = await _httpClient.GetAsync(baseUrl);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<ModelDimJson>();
            }

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
        public async Task<bool> MarkIsTranscriptAsync(string fileName, long loggedInUserId)
        {
            var baseUrl = $"{apiCredential.url}ShowTrancript/Mark-As-Transcripted?fileName={fileName}&loggedInUserId={loggedInUserId}";
            var response = await _httpClient.PostAsync(baseUrl, null);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
    }
}

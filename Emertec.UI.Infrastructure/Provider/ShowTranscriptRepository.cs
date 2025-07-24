using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<ModelDimJson> GetTranscriptsByTelephoneNoAsync(string Telephoneno)
        {
            var baseUrl = apiCredential.url + $"ShowTrancript/sentences-from-telephone?telephone={Telephoneno}";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelDimJson>(json)!;
        }
    }
}

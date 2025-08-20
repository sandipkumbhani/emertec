using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Infrastructure.Provider
{
    public class GetUserNameByIdRepository : IGetUserNameByIdRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public GetUserNameByIdRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<ModelUsers> GetUserNameAsync(long userId)
        {
            var baseUrl = apiCredential.url + $"GetUserName/get-user-name?userId={userId}";
            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"API error {response.StatusCode}: {error}");
            }

            var jsonString = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<ModelUsers>(jsonString);
            return obj!;
        }
    }
}

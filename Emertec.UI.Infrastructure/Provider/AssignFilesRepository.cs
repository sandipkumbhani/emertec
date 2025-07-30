using Emertec.UI.Domain.AssignFiles;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Infrastructure.Provider
{
    public class AssignFilesRepository : IAssignFilesRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public AssignFilesRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<List<ModelUsers>> GetAllUsersAsync()
        {
            var baseUrl = apiCredential.url + "User/get-all-user";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelUsers>>(json)!;
        }
        public async Task<List<string>> GetFileNamesForUserZeroAsync()
        {
            var baseUrl = apiCredential.url + "AssignFile/GetFileNameByUserId";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(json)!;
        }

        public async Task<bool> AssignUserToFilesAsync(int userId, List<Guid> jsonIds)
        {
            var dto = new AssignFilesDto
            {
                UserId = userId,
                jsonid = jsonIds 
            };

            var url = $"{apiCredential.url}AssignFile/AssignUserToFiles"; 

            var jsonContent = new StringContent(
                JsonConvert.SerializeObject(dto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(url, jsonContent);

            return response.IsSuccessStatusCode;
        }



    }
}


using Emertec.UI.Domain.Comman;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Emertec.UI.Infrastructure.Provider
{
    public class MenuMappingRepository : IMenuMappingRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public MenuMappingRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<List<ModelUserMenuMapping>> GetAllMenuMappingAsync()
        {
            var baseUrl = apiCredential.url + "UserMenuMapping/get-all-Menu-Mapping";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelUserMenuMapping>>(json)!;
        }
        public async Task<string> AddMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            var baseUrl = apiCredential.url + "UserMenuMapping/Menu-Master-Mapping";

            var userJson = JsonConvert.SerializeObject(modelUserMenuMapping);
            var requestContent = new StringContent(userJson, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(baseUrl, requestContent);
            var responseData = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
                var message = errorResponse?.ErrorMessage
                              ?? errorResponse?.Message
                              ?? "Failed to create user.";

                throw new Exception($"API Error ({response.StatusCode}): {message}");
            }
            return "Menu Added successfully.";
        }
        public async Task<List<ModelUserRole>> GetAllUserRoleAsync()
        {
            var baseUrl = apiCredential.url + "UserRole/get-all-userRole";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelUserRole>>(json)!;
        }
       

        public async Task<ModelUserMenuMapping> GetMenuMappingByIdAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var baseUrl = apiCredential.url + $"UserMenuMapping/GetMenuMappingById?Menuid={id}";
            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to Menu Mapping user. Status code: {response.StatusCode}");

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelUserMenuMapping>(jsonString)!;
        }

        public async Task<string> UpdateMenuMappingAsync(ModelUserMenuMapping modelUserMenuMapping)
        {
            var baseUrl = $"{apiCredential.url}UserMenuMapping/Update-MenuMasterMapping/{modelUserMenuMapping.UserMenuMappingId}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(modelUserMenuMapping), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(baseUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to update  Menu Mapping. Status code: {response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> DeleteMenuMappingAsync(int id)
        {
            var baseUrl = apiCredential.url + $"UserMenuMapping/Delete-Menu-Mapping/id={id}";
            var response = await _httpClient.DeleteAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to delete Menu Mapping. Status code: {response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }

    }
}

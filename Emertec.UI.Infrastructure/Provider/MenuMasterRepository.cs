using Emertec.UI.Domain.Comman;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Emertec.UI.Infrastructure.Provider
{
    public class MenuMasterRepository : IMenuMasterRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public MenuMasterRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<List<ModelMenuMaster>> GetAllMenuAsync()
        {
            var baseUrl = apiCredential.url + "MenuMaster/Get-All-Menu-Master";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelMenuMaster>>(json)!;
        }
        public async Task<string> AddMenuAsync(ModelMenuMaster modelMenuMaster)
        {
            var baseUrl = apiCredential.url + "MenuMaster/Menu-Master";

            var userJson = JsonConvert.SerializeObject(modelMenuMaster);
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
        public async Task<ModelMenuMaster> GetMenuByIdAsync(int? id)
        {
            var baseUrl = apiCredential.url + $"MenuMaster/{id}";
            var response = await _httpClient.GetAsync(baseUrl);
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelMenuMaster>(jsonString)!;
        }

        public async Task<string> UpdateMenuAsync(ModelMenuMaster menuMaster)
        {
            var baseUrl = apiCredential.url + $"MenuMaster/Update-Menu/{menuMaster.MenuId}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(menuMaster), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(baseUrl, jsonContent);
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> DeletemenuAsync(int id)
        {
            var baseUrl = apiCredential.url + $"MenuMaster/Delete-Menu-Master?id={id}";
            var response = await _httpClient.DeleteAsync(baseUrl);
            return await response.Content.ReadAsStringAsync();
        }
    }
}

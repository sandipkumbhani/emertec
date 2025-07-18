using Emertec.UI.Domain.Comman;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Domain.Models;
using MicroService_Template.Domain.DTO;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Emertec.UI.Infrastructure.Provider
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public UserRepository(HttpClient httpClient, IConfiguration configuration)
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
        public async Task<string> AddUserAsync(ModelUsers user)
        {
            var baseUrl = apiCredential.url + "User/create";

            var userJson = JsonConvert.SerializeObject(user);
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
            return "User created successfully.";
        }
        //user role
        public async Task<List<ModelUserRole>> GetAllUserRoleAsync()
        {
            var baseUrl = apiCredential.url + "UserRole/get-all-userRole";
            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ModelUserRole>>(json)!;
        }
        public async Task<ModelUsers?> GetUserByIdAsync(int userId)
        {
            var baseUrl = apiCredential.url + $"User/GetById?userid={userId}";

            var response = await _httpClient.GetAsync(baseUrl);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelUsers>(json);
        }
        public async Task<string> UpdateUserAsync(ModelUsers model)
        {
            var baseUrl = apiCredential.url + $"User/Update-User?userid={model.UserId}";

            var response = await _httpClient.PutAsJsonAsync(baseUrl, model);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> DeleteUserAsync(int userId)
        {
            var baseUrl = apiCredential.url + $"User/Delete-User?id={userId}";

            var response = await _httpClient.DeleteAsync(baseUrl);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }


    }
}


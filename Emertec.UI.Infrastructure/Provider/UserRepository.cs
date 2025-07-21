using Emertec.UI.Domain.Comman;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using MicroService_Template.Domain.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        public async Task<ModelUsers> GetUsersByIdAsync(int? id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            var baseUrl = apiCredential.url + $"User/GetById?userid={id}";
            var response = await _httpClient.GetAsync(baseUrl);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to get user. Status code: {response.StatusCode}");

            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ModelUsers>(jsonString)!;
        }
        public async Task<string> UpdateUserAsync(ModelUsers user)
        {
            var baseUrl = apiCredential.url + $"User/Update-User/{user.UserId}";
            var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(baseUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to update user. Status code: {response.StatusCode}");

            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> DeleteUserAsync(int id)
        {
            var baseUrl = $"{apiCredential.url}User/Delete-User?id={id}";
            var response = await _httpClient.DeleteAsync(baseUrl);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Failed to delete user. Status code: {response.StatusCode}");
            return await response.Content.ReadAsStringAsync();

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
    }
}


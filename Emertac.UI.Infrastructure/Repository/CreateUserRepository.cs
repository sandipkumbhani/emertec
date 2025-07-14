
using Emertac.UI.Domain.DTO;
using Emertac.UI.Domain.Interface;
using System.Net.Http.Json;

namespace Emertac.UI.Infrastructure.Repository
{
    public class CreateUserRepository : ICreateUserRepository
    {
        private readonly HttpClient _httpClient;

        public CreateUserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterUserAsync(CreateUserDTO createUserdto)
        {
            var response = await _httpClient.PostAsJsonAsync("CreateUser/register", createUserdto);

            return response.IsSuccessStatusCode;
        }
    }
    
}

using Emertac.UI.Application.DTO;
using Emertac.UI.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Emertac.UI.Infrastructure.Repository
{
    public class CreateUserRepository : ICreateUserInterface
    {
        private readonly HttpClient _httpClient;

        public CreateUserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5010"); 
        }

        public async Task<Guid> CreateUserAsync(CreateUserDTO createuserdto)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5010/api/CreateUser/register", createuserdto);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API call failed with status: {response.StatusCode}");
            }
            var result = await response.Content.ReadFromJsonAsync<CreateUserDTO>();

            if (result == null || result.Id == Guid.Empty)
            {
                Console.WriteLine("Invalid API response");
            }

            return result.Id;
        }
    }
    
}

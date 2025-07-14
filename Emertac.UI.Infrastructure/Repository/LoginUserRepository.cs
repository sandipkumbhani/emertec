using Emertac.UI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Emertac.UI.Domain.Interface;

namespace Emertac.UI.Infrastructure.Repository
{
    public class LoginUserRepository: ILoginUserRepository
    {
        private readonly HttpClient _httpClient;

        public LoginUserRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> LoginUserAsync(LoginUserDTO loginUserDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("LoginUsers/login", loginUserDTO);

            return response.IsSuccessStatusCode;
        }
    }
}

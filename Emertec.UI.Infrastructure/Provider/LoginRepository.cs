using Emertec.UI.Domain.Comman;
using Emertec.UI.Domain.Helper;
using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace Emertec.UI.Infrastructure.Provider
{
    public class LoginRepository : ILoginRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private APICredential apiCredential;
        public LoginRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            apiCredential = new APICredential(configuration);
        }
        public async Task<string> CreateUserLoginAsync(LoginViewModel userModel)
        {
            try
            {
                var baseUrl = apiCredential.url + "Login";

                var user = JsonConvert.SerializeObject(userModel);
                var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(baseUrl, requestContent);
                var responseData = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
                if (responseModel != null)
                {
                    var responseToken = JsonConvert.DeserializeObject<ResponseToken>(responseModel?.Data.ToString()!);
                    return responseToken.Token;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return null;
        }

    }
}

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
                string Response = string.Empty;
                var baseUrl = apiCredential.url + "Login/login";
                var user = JsonConvert.SerializeObject(userModel);
                var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(baseUrl, requestContent);
                var responseData = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);
                if (response.IsSuccessStatusCode)
                {
                    if (responseModel != null && responseModel.Data != null)
                    {
                        var jsonData = JsonConvert.SerializeObject(responseModel.Data);
                        var responseToken = JsonConvert.DeserializeObject<ResponseToken>(jsonData);
                        if (responseToken != null && !string.IsNullOrEmpty(responseToken.Token))
                        {
                            Response = responseToken.Token;
                        }
                    }
                }
                else
                {
                    if (responseModel != null && !string.IsNullOrEmpty(responseModel.ErrorMessage))
                    {
                        throw new Exception(responseModel.ErrorMessage);
                    }
                }
                return Response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

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
                var baseUrl = apiCredential.url + "Login/login";

                var user = JsonConvert.SerializeObject(userModel);
                var requestContent = new StringContent(user, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(baseUrl, requestContent);
                var responseData = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);

                    var message = errorResponse?.ErrorMessage ?? errorResponse?.Message ?? "Login failed.";
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new UnauthorizedAccessException(message);
                    }
                    else
                    {
                        throw new Exception($"Login error: {message}");
                    }
                }
                var responseModel = JsonConvert.DeserializeObject<CommanResponseDto>(responseData);

                if (responseModel?.Data == null)
                {
                    Console.WriteLine("Login failed: No data received.");
                }
                var jsonData = JsonConvert.SerializeObject(responseModel?.Data);
                var responseToken = JsonConvert.DeserializeObject<ResponseToken>(jsonData);

                return responseToken?.Token ?? throw new Exception("Token not found in response.");
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Unauthorized: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Login Error: " + ex.Message);
            }
            return "Something Is Wrong !";
        }
    }
}

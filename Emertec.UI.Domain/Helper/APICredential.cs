using Microsoft.Extensions.Configuration;

namespace Emertec.UI.Domain.Helper
{
    public class APICredential
    {
        public string? url { get; set; }
        public APICredential(IConfiguration configuration)
        {
            var qurtzsetting = configuration.GetSection("APICredential");
            url = qurtzsetting.GetSection("url").Value;
        }
    }
}

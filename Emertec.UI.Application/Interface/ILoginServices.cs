using Emertec.UI.Domain.Models;

namespace Emertec.UI.Application.Interface
{
    public interface ILoginServices
    {
        Task<ResponseToken> Login(LoginViewModel model);
    }
}


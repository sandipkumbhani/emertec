using Emertec.UI.Domain.Models;

namespace Emertec.UI.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<string> CreateUserLoginAsync(LoginViewModel userModel);
    }
}

using Emertac.UI.Domain.DTO;
namespace Emertac.UI.Domain.Interface
{
    public interface ILoginUserRepository
    {
        Task<bool> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emertac.UI.Domain.DTO;

namespace Emertac.UI.Application.Interface
{
    public interface ILoginUserService
    {
        Task<bool> LoginUserAsync(LoginUserDTO loginUserDTO);
    }
}

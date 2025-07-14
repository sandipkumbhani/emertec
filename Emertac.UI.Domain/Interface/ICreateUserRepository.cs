using Emertac.UI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertac.UI.Domain.Interface
{
    public interface ICreateUserRepository
    {
        Task<bool> RegisterUserAsync(CreateUserDTO createUserdto);
    }
}

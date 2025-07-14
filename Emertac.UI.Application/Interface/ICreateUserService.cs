using Emertac.UI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertac.UI.Application.Interface
{
    public interface ICreateUserService
    {
        Task<bool> AddUser(CreateUserDTO createUserDTO);
    }
}

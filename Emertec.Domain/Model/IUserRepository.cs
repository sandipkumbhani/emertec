using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Domain.Model
{
    public interface IUserRepository
    {
        Task<ModelUserLogin> GetByEmailAsync(string email);
        Task AddAsync(ModelUserLogin user);
    }
}

using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Template.Application.Extension.Interface
{
    public interface IForgotPasswordService
    {
        Task<ModelUsers> CheckEmailidAsync(string email);
        Task<ModelUsers> UpdatePasswordAsync(string email, ModelUsers modelUsers);
    }
}

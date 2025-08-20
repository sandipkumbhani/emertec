using Emertec.UI.Application.Interface;
using Emertec.UI.Domain.Interfaces;
using Emertec.UI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emertec.UI.Application.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IResetPasswordRepossitory _resetPasswordRepository;
        public ResetPasswordService(IResetPasswordRepossitory resetPasswordRepository)
        {
            _resetPasswordRepository = resetPasswordRepository;
        }
        public async Task<string> ResetPassworsdAsync(ResetPasswordModel resetPasswordModel)
        {
           return await _resetPasswordRepository.UpdatePasswordAsync(resetPasswordModel);
        }
    }
}

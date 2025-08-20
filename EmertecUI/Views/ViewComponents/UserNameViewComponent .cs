using Emertec.UI.Application.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Views.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly IGetUserNameByIdService _getUserNameByIdService;
        public UserNameViewComponent(IGetUserNameByIdService getUserNameByIdService)
        {
            _getUserNameByIdService = getUserNameByIdService ?? throw new ArgumentNullException(nameof(getUserNameByIdService));
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userName = null;

            var userIdClaim = HttpContext.User?.FindFirst("UserId")?.Value;

            if (!string.IsNullOrEmpty(userIdClaim) && long.TryParse(userIdClaim, out long userId))
            {
                var user = await _getUserNameByIdService.GetUserNameByIdAsync(userId);
                if (user != null && !string.IsNullOrEmpty(user.Name))
                {
                    userName = user.Name;
                }
            }
            return View("Default", userName);
        }

    }
}

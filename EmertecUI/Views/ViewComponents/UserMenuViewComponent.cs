using Emertec.UI.Application.Interface;
using MicroService_Template.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Views.ViewComponents
{
    public class UserMenuViewComponent : ViewComponent
    {
        private readonly IMenuMasterServices _menuService;

        public UserMenuViewComponent(IMenuMasterServices menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId")?.Value;
            if (!long.TryParse(userIdClaim, out long userId))
            {
                return View(new List<ModelMenuMaster>());
            }

            var menus = await _menuService.GetMenusByUserIdAsync(Convert.ToInt64(userIdClaim));
            return View(menus);
        }
    }
}

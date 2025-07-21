
using Emertec.UI.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EmertecUI.Views.Shared.ViewComponent
{
    public class SidebarMenuViewComponent
    {
         private readonly IMenuMasterServices _menuMappingService;

        public SidebarMenuViewComponent(IMenuMasterServices menuMappingService)
        {
            _menuMappingService = menuMappingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userIdClaim = HttpContext.User.FindFirst("UserId");

            if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out long userId))
                return View(new List<ModelMenuMaster>());

            var menus = await _menuMappingService.GetMenusByUserIdAsync(userId);
            return View(menus);
        }
    }
    }
}

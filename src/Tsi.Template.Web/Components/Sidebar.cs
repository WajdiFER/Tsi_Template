using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tsi.Template.Web.Factories.Common;

namespace SageClient.Components
{
    public class Sidebar: ViewComponent
    {
        private readonly ISidebarModelFactory _sidebarModelFactory;

        public Sidebar(ISidebarModelFactory sidebarModelFactory)
        {
            _sidebarModelFactory = sidebarModelFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedTab)
        {
            var model = await _sidebarModelFactory.PrepareMenuItemsAsync(selectedTab);

            return await Task.FromResult((IViewComponentResult)View(model));
        }
    }
}

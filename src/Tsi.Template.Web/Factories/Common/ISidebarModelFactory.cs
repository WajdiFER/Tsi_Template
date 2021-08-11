using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Core;

namespace Tsi.Template.Web.Factories.Common
{
    public interface ISidebarModelFactory
    {
        Task<IList<MenuItem>> PrepareMenuItemsAsync(int selectedTab);
    }
}
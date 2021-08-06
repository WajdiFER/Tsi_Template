using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsi.Template.ViewModels.Catalog;

namespace Tsi.Template.Web.Factories.Catalog
{
    public interface IProductModelFactory
    {
        Task<ProductViewModel> PrepareProductViewModelAsync();
    }
}

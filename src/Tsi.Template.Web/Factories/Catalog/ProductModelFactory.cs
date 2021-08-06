using System;
using System.Threading.Tasks;
using Tsi.Template.Core.Attributes;
using Tsi.Template.ViewModels.Catalog;

namespace Tsi.Template.Web.Factories.Catalog
{
    [Injectable(typeof(IProductModelFactory))]
    public class ProductModelFactory : IProductModelFactory
    {
        public Task<ProductViewModel> PrepareProductViewModelAsync()=> Task.FromResult(new ProductViewModel());
    }
}

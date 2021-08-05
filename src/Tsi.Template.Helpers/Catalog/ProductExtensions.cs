using System.Collections.Generic;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.ViewModels.Catalog;
using System.Linq; 

namespace Tsi.Template.Helpers.Catalog
{
    public static class ProductExtensions
    {
        public static Product ToProduct(this ProductViewModel model) => new()
        {
            Code = model.Code,
            Libelle = model.Libelle,
            Price = model.Price,
        };

        public static ProductViewModel ToViewModel(this Product product) => new()
        {
            Id = product.Id,
            Code = product.Code,
            Libelle = product.Libelle,
            Price = product.Price
        };

        public static IEnumerable<Product> ToProducts(this IEnumerable<ProductViewModel> models)
            => models.Select(ToProduct);

        public static IEnumerable<ProductViewModel> ToViewModels(this IEnumerable<Product> products)
            => products.Select(ToViewModel); 
    }
}

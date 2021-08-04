using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Domain.Gesc.Catalog;

namespace Tsi.Template.Abstraction.Catalog
{
    public interface IProductService
    {
        public Task<Product> CreateProductAsync(Product product);

        public Task DeleteProductAsync(int id);
        Task<IAsyncEnumerable<Product>> GetAllAsync();
    }
}

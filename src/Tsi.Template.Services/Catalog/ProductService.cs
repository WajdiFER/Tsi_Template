using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Domain.Logging;
using Tsi.Template.Infrastructure.Repository;
using Tsi.Template.ViewModels.Catalog;

namespace Tsi.Template.Services.Catalog
{
    [Injectable(typeof(IProductService))]
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepo; 

        public ProductService(IRepository<Product> productRepo)
        {
            _productRepo = productRepo; 
        }

        public async Task<Product> CreateProductAsync(Product product)
        { 
            return await _productRepo.AddAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            await _productRepo.DeleteAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepo.GetAllAsync();
        }

        public async Task<Product> GetProductbyCode(string code)
        {
            return await _productRepo.GetAsync(p => p.Code.Equals(code));
        }

        public async Task<Product> GetProductbyId(int id)
        {
            return await _productRepo.GetByIdAsync(id);
        }

        public async Task UpdateProductAsync(ProductViewModel model)
        {     
            var product = await GetProductbyId(model.Id);

            if( product is null)
            {
                return;
            }

            product.Libelle = model.Libelle;
            product.Price = model.Price;
            await _productRepo.UpdateAsync(product);
        }
    } 
}

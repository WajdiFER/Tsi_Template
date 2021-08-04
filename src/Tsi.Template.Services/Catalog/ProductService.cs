using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Core.Events;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Infrastructure.Repository;

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

        public async Task<IAsyncEnumerable<Product>> GetAllAsync()
        {
            return await _productRepo.GetAllAsync();
        }
    }

    public class IProductCreatedConsumer : IConsumer<EntityInsertedEvent<Product>>
    {
        public Task HandleEventAsync(EntityInsertedEvent<Product> eventMessage)
        {
            var product = eventMessage.Entity;

            return Task.CompletedTask;
        }
    }

    public class IProductDeletedConsumer : IConsumer<EntityDeletedEvent<Product>>
    {
        public Task HandleEventAsync(EntityDeletedEvent<Product> eventMessage)
        {
            var deletedProduct = eventMessage.Entity;

            return Task.CompletedTask;
        }
    }
}

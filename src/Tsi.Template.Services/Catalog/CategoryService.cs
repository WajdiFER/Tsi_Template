using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Abstraction.Catalog;
using Tsi.Template.Core.Attributes;
using Tsi.Template.Domain.Gesc.Catalog;
using Tsi.Template.Infrastructure.Repository;

namespace Tsi.Template.Services.Catalog
{
    [Injectable(typeof(ICategoryService))]
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepo;

        public CategoryService(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public async Task CreateCategoryAsync(Category entity)
        {
            await _categoryRepo.AddAsync(entity);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepo.GetAllAsync();
        }
    }
}

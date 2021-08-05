using System.Collections.Generic;
using System.Threading.Tasks;
using Tsi.Template.Domain.Gesc.Catalog;

namespace Tsi.Template.Abstraction.Catalog
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Category entity);

        Task<IEnumerable<Category>> GetAllAsync();
    }
}

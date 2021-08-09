using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tsi.Template.Core.Abstractions;

namespace Tsi.Template.Infrastructure.Repository
{
    public interface IRepository<T>  where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<int> DeleteAsync(Expression<Func<T, bool>> where);
        Task<int> DeleteAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> where); 
        Task<T> GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> orderBy = null); 
        Task<int> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetManyWithIncludeAsync(Expression<Func<T, bool>> where = null, Expression<Func<T, bool>> orderBy = null, Expression<Func<T, object>> include= null);
    }
}

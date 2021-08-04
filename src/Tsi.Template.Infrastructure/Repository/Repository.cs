using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Core.Events;
using Tsi.Template.Infrastructure.Data;

namespace Tsi.Template.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly DbSet<T> dbset;
        private readonly ApplicationContext _context;
        private readonly IEventPublisher _eventPublisher;

        public Repository(IDatabaseFactory dbFactory, IEventPublisher eventPublisher)
        {
            _context = dbFactory.DataContext;
            dbset = _context.Set<T>();
            _eventPublisher = eventPublisher;
        }

        public async Task<T> AddAsync(T entity)
        {
            var InsertResult = await dbset.AddAsync(entity);

            await _context.SaveChangesAsync();

            await _eventPublisher.PublishAsync(new EntityInsertedEvent<T>(InsertResult.Entity));

            return InsertResult.Entity;
        }

        public async Task<int> DeleteAsync(Expression<Func<T, bool>> @where)
        {
            await foreach (var entity in dbset.Where<T>(where).AsAsyncEnumerable())
            {
                InternalDelete(entity);

                await _eventPublisher.PublishAsync(new EntityDeletedEvent<T>(entity));
            }

            // if we find no entry in the database, we return 0 as in 0 rows updated
            return await _context.CommitAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            InternalDelete(entity);

            await _eventPublisher.PublishAsync(new EntityDeletedEvent<T>(entity));

            return await _context.CommitAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> @where)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return await dbset.Where(where).Where(T => !((ISoftDelete)T).Deleted).FirstOrDefaultAsync<T>();
            }
            else
            {
                return await dbset.Where(where).FirstOrDefaultAsync<T>();
            }

        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return await dbset.Where(T => !((ISoftDelete)T).Deleted && T.Id == id).FirstOrDefaultAsync<T>();
            }
            else
            {
                return await dbset.FindAsync(id);
            }

        }

        public Task<IAsyncEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> @where = null, Expression<Func<T, bool>> orderBy = null)
        {
            IQueryable<T> query = dbset;
            if (where != null)
            {
                query = query.Where(where);
            }

            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(T => !((ISoftDelete)T).Deleted);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return Task.FromResult(query.AsAsyncEnumerable());
        }

        public async Task<int> UpdateAsync(T entity)
        {
            dbset.Update(entity);

            var result = await _context.CommitAsync();

            await _eventPublisher.PublishAsync(new EntityUpdatedEvent<T>(entity));

            return result;
        }

        public Task<IAsyncEnumerable<T>> GetAllAsync()
        {
            if (typeof(ISoftDelete).IsAssignableFrom(typeof(T)))
            {
                return Task.FromResult(dbset.Where(T => !((ISoftDelete)T).Deleted).AsAsyncEnumerable());
            }
            else
            {
                return Task.FromResult(dbset.AsAsyncEnumerable());
            }   
        }



        #region Private methods 
        private void InternalDelete(T entity)
        {
            if (entity is ISoftDelete)
            {
                SoftDelete(entity);
            }
            else
            {
                HardDeleteEntity(entity);
            }
        }

        private void HardDeleteEntity(T entity)
        {
            dbset.Remove(entity);
        }

        private void SoftDelete(T entity)
        {
            ((ISoftDelete)entity).Deleted = true;
            ((ISoftDelete)entity).DeletedAt = DateTime.Now;

            dbset.Update(entity);
        }
        #endregion
    }
}

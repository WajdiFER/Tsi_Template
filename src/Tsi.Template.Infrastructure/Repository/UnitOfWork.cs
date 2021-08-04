using System.Threading.Tasks;
using Tsi.Template.Core.Abstractions;
using Tsi.Template.Infrastructure.Data;

namespace Tsi.Template.Infrastructure.Repository
{
    //public class UnitOfWork : IUnitOfWork
    //{
    //    #region Fields

    //    private readonly ApplicationContext _dataContext;
    //    readonly IDatabaseFactory _dbFactory;


    //    #endregion

    //    #region Constructor

    //    public UnitOfWork(IDatabaseFactory dbFactory)
    //    {
    //        _dbFactory = dbFactory;
    //        _dataContext = dbFactory.DataContext;
    //    }

    //    #endregion

    //    #region Methods

    //    public void Dispose()
    //    {
    //        _dataContext.Dispose();
    //    }

    //    public IRepository<T> GetRepository<T>() where T : BaseEntity
    //    {
    //        IRepository<T> repo = new Repository<T>(_dbFactory);
    //        return repo;
    //    }

    //    public Task<int> CommitWithoutAuditAsync()
    //    {
    //        return _dataContext.SaveChangesAsync();
    //    }

    //    #endregion
    //}
}

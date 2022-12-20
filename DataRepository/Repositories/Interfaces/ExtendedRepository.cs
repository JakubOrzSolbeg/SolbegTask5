using DataRepository.DbContext;
using DataRepository.Entities.Base;

namespace DataRepository.Repositories.Interfaces;

public abstract class ExtendedRepository<T> : Repository<T> where T : BaseEntity
{
    public ExtendedRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
        
    }

    public abstract Task<List<T>> GetRange(List<int> ids);
    public abstract Task<T?> GetByPredicate(Predicate<T> predicate);
    public abstract Task<List<TResult>> GetAllAndSelect<TResult>(Func<T, TResult> selector);
}
using DataRepository.DbContext;
using DataRepository.Entities.Base;

namespace DataRepository.Repositories.Interfaces;

public abstract class NestedRepository<T> : ExtendedRepository<T> where T : BaseEntity
{
    public NestedRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
        
    }
    public abstract Task<TResult?> GetOneNested<TResult>(int id, Func<T, TResult> selector);
    public abstract Task<List<TResult>> GetListNested<TResult>(List<int> ids, Func<T, TResult> selector);

    public abstract Task<List<TResult>>
        GetNestedByPredicate<TResult>(Predicate<T> predicate, Func<T, TResult> selector);
}

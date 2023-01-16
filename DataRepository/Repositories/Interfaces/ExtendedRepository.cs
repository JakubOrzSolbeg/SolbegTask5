using DataRepository.DbContext;
using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Interfaces;

public class ExtendedRepository<T> : Repository<T> where T : BaseEntity
{
    public ExtendedRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
        
    }

    public async Task<List<T>> GetRange(List<int> ids)
    {
        var idSet = ids.ToHashSet();
        return await MainDbContext.Set<T>()
            .Where(item => idSet.Contains(item.Id))
            .ToListAsync();
    }

    public async Task<T?> GetByPredicate(Predicate<T> predicate)
    {
        T? result = null;
        await Task.Run(() =>
        {
            result = MainDbContext.Set<T>()
                .AsEnumerable()
                .FirstOrDefault(item => predicate(item));
        });
        return result;
    }

    public async Task<List<T>> GetAllByPredicate(Predicate<T> predicate)
    {
        List<T> result = new List<T>();
        await Task.Run(() =>
        {
            result = MainDbContext.Set<T>()
                .AsEnumerable()
                .Where(obj => predicate(obj))
                .ToList();
        });
        return result;
    }

    public async Task<List<TResult>> GetAllAndSelect<TResult>(Func<T, TResult> selector)
    {
        return await MainDbContext.Set<T>()
            .Select(product => selector(product))
            .ToListAsync();
    }
    
}
using DataRepository.DbContext;
using DataRepository.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Interfaces;

public class Repository<T> where T : BaseEntity
{
    protected readonly MainDbContext1 MainDbContext;

    public Repository(MainDbContext1 mainDbContext)
    {
        MainDbContext = mainDbContext;
    }

    public async Task<List<T>> GetAll()
    {
        return await MainDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await MainDbContext.Set<T>()
            .FirstOrDefaultAsync(o => o.Id.Equals(id));
    }

    public async Task<T> Add(T obj)
    {
        MainDbContext.Set<T>()
            .Add(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<T> Update(T obj)
    {
        MainDbContext.Set<T>()
            .Update(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<bool> Delete(T obj)
    {
        MainDbContext.Set<T>().Remove(obj);
        await MainDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> Count()
    {
        return await MainDbContext.Set<T>().CountAsync();
    }

    public async Task Commit()
    {
        await MainDbContext.SaveChangesAsync();
    }
}
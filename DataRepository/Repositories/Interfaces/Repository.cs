using DataRepository.DbContext;
using DataRepository.Entities.Base;

namespace DataRepository.Repositories.Interfaces;

public abstract class Repository<T> where T : BaseEntity
{
    protected readonly MainDbContext1 MainDbContext;

    public Repository(MainDbContext1 mainDbContext)
    {
        MainDbContext = mainDbContext;
    }

    public abstract Task<List<T>> GetAll();
    public abstract Task<T?> GetById(int id);
    public abstract Task<T> Add(T obj);
    public abstract Task<T> Update(T obj);
    public abstract Task<bool> Delete(T obj);

    public async Task Commit()
    {
        await MainDbContext.SaveChangesAsync();
    }
}
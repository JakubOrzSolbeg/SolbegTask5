using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class AccountRepository : ExtendedRepository<User>
{
    public AccountRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }

    public override async Task<List<User>> GetAll()
    {
        return await MainDbContext.Users.ToListAsync();
    }

    public override async Task<User?> GetById(int id)
    {
        return await MainDbContext.Users.FirstOrDefaultAsync(user => user.Id.Equals(id));
    }

    public override async Task<User> Add(User obj)
    {
        await MainDbContext.Users.AddAsync(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<User> Update(User obj)
    {
        MainDbContext.Users.Update(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<bool> Delete(User obj)
    {
        MainDbContext.Users.Remove(obj);
        await MainDbContext.SaveChangesAsync();
        return true;
    }

    public override async Task<List<User>> GetRange(List<int> ids)
    {
        var idSet = ids.ToHashSet();
        return await MainDbContext.Users.Where(user => idSet.Contains(user.Id)).ToListAsync();
    }

    public override async Task<User?> GetByPredicate(Predicate<User> predicate)
    {
        User? result = null;
        await Task.Run(() =>
        {
            result = MainDbContext.Users.AsEnumerable()
                .FirstOrDefault(user => predicate(user));
        });
        return result;
    }

    public override async Task<List<TResult>> GetAllAndSelect<TResult>(Func<User, TResult> selector)
    {
        return await MainDbContext.Users.Select(user => selector(user)).ToListAsync();
    }
}
using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class OrderRepository : NestedRepository<Order>
{
    public OrderRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }
    
    public override async Task<TResult?> GetOneNested<TResult>(int id, Func<Order, TResult> selector) where TResult : default
    {
        var result = await MainDbContext.Orders
            .Where(o => o.Id.Equals(id))
            .Include(o => o.User)
            .Include(o => o.OrderProducts)
            .ThenInclude(oo => oo.Product)
            .Select(o => selector(o))
            .FirstOrDefaultAsync();
            
        return result;
    }

    public override async Task<List<TResult>> GetListNested<TResult>(List<int> ids, Func<Order, TResult> selector)
    {
        var idSet = ids.ToHashSet();
        return await MainDbContext.Orders
            .Where(o => idSet.Contains(o.Id))
            .Include(o => o.User)
            .Include(o => o.OrderProducts)
            .ThenInclude(oo => oo.Product)
            .Select(o => selector(o))
            .ToListAsync();
    }

    public override async Task<List<TResult>> GetNestedByPredicate<TResult>(Predicate<Order> predicate, Func<Order, TResult> selector)
    {
        List<TResult> result = new List<TResult>();
        await Task.Run(() =>
        {
            result = MainDbContext.Orders
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                .ThenInclude(oo => oo.Product)
                .AsEnumerable()
                .Where(order => predicate(order))
                .Select(order => selector(order))
                .ToList();
        });
        return result;
    }
}
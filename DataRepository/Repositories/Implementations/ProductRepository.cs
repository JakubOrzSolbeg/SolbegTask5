using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class ProductRepository : ExtendedRepository<Product>
{
    public ProductRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }

    public override async Task<List<Product>> GetAll()
    {
        return await MainDbContext.Products.ToListAsync();
    }

    public override async Task<Product?> GetById(int id)
    {
        return await MainDbContext.Products.FirstOrDefaultAsync(product => product.Id.Equals(id));
    }

    public override async Task<Product> Add(Product obj)
    {
        await MainDbContext.AddAsync(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<Product> Update(Product obj)
    {
        MainDbContext.Products.Update(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<bool> Delete(Product obj)
    {
        MainDbContext.Products.Remove(obj);
        await MainDbContext.SaveChangesAsync();
        return true;
    }

    public override async Task<List<Product>> GetRange(List<int> ids)
    {
        var idSet = ids.ToHashSet();
        return await MainDbContext.Products
            .Where(product => idSet.Contains(product.Id))
            .ToListAsync();
    }

    public override async Task<Product?> GetByPredicate(Predicate<Product> predicate)
    {
        return await MainDbContext.Products.FirstOrDefaultAsync(product => predicate(product));
    }

    public override async Task<List<TResult>> GetAllAndSelect<TResult>(Func<Product, TResult> selector)
    {
        return await MainDbContext.Products
            .Select(product => selector(product))
            .ToListAsync();
    }
}
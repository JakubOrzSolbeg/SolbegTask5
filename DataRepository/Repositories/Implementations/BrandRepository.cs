using DataRepository.DbContext;
using DataRepository.Entities;
using DataRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataRepository.Repositories.Implementations;

public class BrandRepository : Repository<Brand>
{
    public BrandRepository(MainDbContext1 mainDbContext) : base(mainDbContext)
    {
    }

    public override async Task<List<Brand>> GetAll()
    {
        return await MainDbContext.Brands.ToListAsync();
    }

    public override async Task<Brand?> GetById(int id)
    {
        return await MainDbContext.Brands.FirstOrDefaultAsync(brand => brand.Id.Equals(id));
    }

    public override async Task<Brand> Add(Brand obj)
    {
        await MainDbContext.Brands.AddAsync(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<Brand> Update(Brand obj)
    {
        MainDbContext.Brands.Update(obj);
        await MainDbContext.SaveChangesAsync();
        return obj;
    }

    public override async Task<bool> Delete(Brand obj)
    {
        MainDbContext.Remove(obj);
        await MainDbContext.SaveChangesAsync();
        return true;
    }
}
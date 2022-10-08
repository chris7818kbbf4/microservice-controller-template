using EventCatalog.Entities.Base;
using Microsoft.EntityFrameworkCore;
using ServiceTemplate.DbContexts;

namespace ServiceTemplate.Repositories.Base;

public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : class, IIdentity
{
    protected readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TModel?> GetById(Guid id)
    {
        return await _context.Set<TModel>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TModel> Add(TModel model)
    {
        var result = await _context.Set<TModel>().AddAsync(model);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<TModel> Update(TModel model)
    {
        var result = _context.Set<TModel>().Update(model);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity is null) return;

        _context.Set<TModel>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
using EventCatalog.Entities.Base;
using Microsoft.EntityFrameworkCore;
using ServiceTemplate.DbContexts;

namespace ServiceTemplate.Repositories.Base;

public abstract class BaseRepository<TModel>: IBaseRepository<TModel> where TModel : class, IIdentity
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

        return result.Entity;
    }

    public TModel Update(TModel model)
    {
        var result = _context.Set<TModel>().Update(model);

        return result.Entity;
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _context.Set<TModel>().Remove(entity);
        }
    }
}
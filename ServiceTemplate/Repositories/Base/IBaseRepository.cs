namespace ServiceTemplate.Repositories.Base;

public interface IBaseRepository<TModel>
{
    public Task<TModel?> GetById(Guid id);
    
    public Task<TModel> Add(TModel model);
    
    public TModel Update(TModel model);
    
    Task Delete(Guid id);
}
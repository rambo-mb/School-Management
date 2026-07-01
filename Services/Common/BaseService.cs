using SM.Models.Common;
using SM.Repositories.Common;

namespace SM.Services.Common;

public class BaseService<T> : IGenericService<T> where T : IModel
{
    private IRepository<T> _repository;

    public BaseService(IRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(T item)
    {
        await _repository.AddAsync(item);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public List<T> GetAll()
    {
        return _repository.GetAll();
    }

    public T GetById(int id)
    {
        return _repository.GetById(id);
    }

    public int GetCount()
    {
        return GetAll().Count;
    }

    public List<T> GetPaginatedItems(int pageNumber, int pageSize)
    {
        return GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
    }

    public async Task UpdateAsync(T item)
    {
        await _repository.UpdateAsync(item);
    }
}
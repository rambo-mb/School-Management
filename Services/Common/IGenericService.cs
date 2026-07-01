using SM.Models.Common;

namespace SM.Services.Common;

public interface IGenericService<T> where T : IModel
{
    List<T> GetAll();
    T GetById(int id);
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(int id);
    List<T> GetPaginatedItems(int pageNumber, int pageSize);
    int GetCount();
}

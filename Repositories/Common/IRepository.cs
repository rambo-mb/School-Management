using SM.Models.Common;

namespace SM.Repositories.Common;

public interface IRepository<T> where T : IModel
{
    List<T> GetAll();
    T GetById(int id);
    Task AddAsync(T item);
    Task UpdateAsync(T item);
    Task DeleteAsync(int id);
}
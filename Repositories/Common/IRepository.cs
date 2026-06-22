using SM.Models.Common;

namespace SM.Repositories.Common;

public interface IRepository<T> where T : IModel
{
	List<T> GetAll();
	T GetById(int id);
	void Add(T item);
	void Update(T item);
	void Delete(int id);
}
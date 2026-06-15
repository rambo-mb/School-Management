using SM.Models.Common;

namespace SM.Services.Common;

public interface IGenericService<T> where T : IModel
{
	List<T> GetAll();
	T GetById(int id);
	void Add(T item);
	void Update(T item);
	void Delete(int id);
	List<T> GetPaginatedItems(int pageNumber, int pageSize);
	int GetCount();
}

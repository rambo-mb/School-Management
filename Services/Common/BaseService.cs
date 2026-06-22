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

	public void Add(T item)
	{
		_repository.Add(item);
	}

	public void Delete(int id)
	{
		_repository.Delete(id);
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

	public void Update(T item)
	{
		_repository.Update(item);
	}
}
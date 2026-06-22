using SM.Exceptions;
using System.Text.Json;
using SM.Models.Common;

namespace SM.Repositories.Common;

public abstract class BaseRepository<T> : IRepository<T> where T : IModel
{

    protected List<T> items;
    private readonly string _filePath;
    private int _nextId;

    public BaseRepository(string filePath)
    {
        _filePath = filePath;
        items = LoadItems(_filePath);
        _nextId = items.Count == 0 ? 1 : items.Max(item => item.Id) + 1;
    }

    private List<T> LoadItems(string path)
    {
        if (!File.Exists(path))
        {
            FileStream fileStream = File.Create(path);
            fileStream.Close();

            items = new List<T>();
        }
        else
        {
            string content = File.ReadAllText(path);

            items = JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();
        }

        return items;
    }

    protected void SaveItems()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string content = JsonSerializer.Serialize(items, options);
        File.WriteAllText(_filePath, content);
    }

    public List<T> GetAll()
    {
        return items;
    }

    public T GetById(int id)
    {
        T item = items.FirstOrDefault(item => item.Id == id);

        if (item is null)
            throw new NotFoundException($"Item with ID {id} not found");

        return item;
    }

    protected abstract void ValidateItem(T item);

    public void Add(T item)
    {
        if (item is null)
            throw new ValidationException("Item cannot be null");

        ValidateItem(item);

        item.Id = _nextId++;
        items.Add(item);
        SaveItems();
    }

    public abstract void Update(T item);

    public void Delete(int id)
    {
        items.RemoveAll(item => item.Id == id);
        SaveItems();
    }
}
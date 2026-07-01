using System.Text.Json;
using SM.Exceptions;
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

    protected async Task SaveItemsAsync()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string content = JsonSerializer.Serialize(items, options);
        await File.WriteAllTextAsync(_filePath, content);
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

    public async Task AddAsync(T item)
    {
        if (item is null)
            throw new ValidationException("Item cannot be null");

        ValidateItem(item);

        item.Id = _nextId++;
        items.Add(item);
        await SaveItemsAsync();
    }

    public abstract Task UpdateAsync(T item);

    public async Task DeleteAsync(int id)
    {
        items.RemoveAll(item => item.Id == id);
        await SaveItemsAsync();
    }
}
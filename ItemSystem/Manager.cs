using System.Text.Json;

namespace ItemSystem;

public class Manager<T> where T : class
{
    private readonly ICollection<T> Items;

    public Manager()
    {
        Items = new HashSet<T>();
    }

    public IEnumerable<T> Get()
    {
        return Items;
    }

    public void Clear()
    {
        Items.Clear();
    }

    public void Load(string itemTypesFilePath)
    {
        var json = Manager<T>.LoadFile(itemTypesFilePath);
        var items = Manager<T>.ParseJson(json);

        foreach (var item in items)
        {
            Items.Add(item);
        }
    }

    private static string LoadFile(string filepath)
    {
        return File.ReadAllText(filepath);
    }

    private static IEnumerable<T> ParseJson(string json)
    {
        var result = JsonSerializer.Deserialize<T[]>(json);
        return result ?? throw new ApplicationException($"Failed to parse json.");
    }
}
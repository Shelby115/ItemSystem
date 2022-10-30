using System.Text.Json;

namespace ItemSystem;

public class Manager<T> : IEnumerable<T> where T : class
{
    private readonly ICollection<T> Items;

    public Manager()
    {
        Items = new HashSet<T>();
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

    public IEnumerator<T> GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return Items.GetEnumerator();
    }
}
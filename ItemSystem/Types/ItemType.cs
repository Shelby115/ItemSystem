namespace ItemSystem.Types;

/// <summary>
/// A name and description for an item (e.g., "Dagger, a small blade with a small handle.").
/// </summary>
public class ItemType
{
    public string Name { get; }
    public string Description { get; }

    public ItemType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
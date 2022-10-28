namespace ItemSystem;

public class ItemProperty
{
    public string Name { get; }
    public string Description { get; }

    public ItemProperty(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
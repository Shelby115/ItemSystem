namespace ItemSystem;

public class ItemProperty
{
    public string Name { get; }
    public string Description { get; }
    public ICollection<ItemPropertyAttributeValue> Attributes { get; }

    public ItemProperty(string name, string description)
    {
        Name = name;
        Description = description;
        Attributes = new List<ItemPropertyAttributeValue>();
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
namespace ItemSystem;

/// <summary>
/// An attribute type is simply a name and description for an item property attribute.
/// For example, the attribute type "Number of uses" will be the same attribute type regardless if it is applied to the "Poisoned" or "Shiny" item properties.
/// </summary>
public class ItemPropertyAttributeType
{
    public string Name { get; }
    public string Description { get; }

    public ItemPropertyAttributeType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
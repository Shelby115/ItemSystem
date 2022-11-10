namespace ItemSystem.Types;

/// <summary>
/// A name, description, and attributes for a property that could be applied to an item.
/// For example, you could have the property "Shiny" applied to an item with a "Number of Uses" of 5.
/// </summary>
public class ItemPropertyType
{
    public string Name { get; }
    public string Description { get; }

    public IEnumerable<ItemPropertyAttributeTypeDefaultValue> AttributeTypes { get; }

    public ItemPropertyType(string name, string description, IEnumerable<ItemPropertyAttributeTypeDefaultValue> attributeTypes)
    {
        Name = name;
        Description = description;
        AttributeTypes = attributeTypes;
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
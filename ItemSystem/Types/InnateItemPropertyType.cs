namespace ItemSystem.Types;

/// <summary>
/// A property type name that should be added to items of the specified item type when they are created.
/// </summary>
public class InnateItemPropertyType
{
    public string ItemType { get; }
    public string PropertyType { get; }

    public InnateItemPropertyType(string itemType, string propertyType)
    {
        ItemType = itemType;
        PropertyType = propertyType;
    }
}
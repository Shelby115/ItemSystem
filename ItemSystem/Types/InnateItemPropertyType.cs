namespace ItemSystem.Types;

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
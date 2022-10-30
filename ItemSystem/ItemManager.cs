namespace ItemSystem;

public class ItemManager
{
    public static readonly Manager<ItemType> Types;
    public static readonly Manager<ItemProperty> Properties;
    public static readonly Manager<ItemInteractionType> InteractionTypes;
    public static readonly Manager<ItemPropertyAttribute> Attributes;
    public static readonly Manager<ItemPropertyAttributeType> AttributeTypes;

    static ItemManager()
    {
        Types = new Manager<ItemType>();
        Types.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");

        Properties = new Manager<ItemProperty>();
        Properties.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemProperties.json");

        InteractionTypes = new Manager<ItemInteractionType>();
        InteractionTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");

        AttributeTypes = new Manager<ItemPropertyAttributeType>();
        AttributeTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyAttributeTypes.json");

        Attributes = new Manager<ItemPropertyAttribute>();
        Attributes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyAttributes.json");
    }
}
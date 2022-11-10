using ItemSystem.Types;

namespace ItemSystem;

public class ItemManager
{
    public static readonly Manager<ItemType> ItemTypes;
    public static readonly Manager<ItemPropertyType> PropertyTypes;
    public static readonly Manager<ItemPropertyAttributeType> AttributeTypes;
    public static readonly Manager<ItemInteractionType> InteractionTypes;

    static ItemManager()
    {
        ItemTypes = new Manager<ItemType>();
        ItemTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");

        AttributeTypes = new Manager<ItemPropertyAttributeType>();
        AttributeTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyAttributeTypes.json");

        PropertyTypes = new Manager<ItemPropertyType>();
        PropertyTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyTypes.json");

        InteractionTypes = new Manager<ItemInteractionType>();
        InteractionTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");
    }
}
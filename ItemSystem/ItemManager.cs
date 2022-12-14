using ItemSystem.Types;

namespace ItemSystem;

public class ItemManager
{
    public static readonly Manager<ItemType> ItemTypes;
    public static readonly Manager<PropertyType> PropertyTypes;
    public static readonly Manager<AttributeType> AttributeTypes;
    public static readonly Manager<InteractionType> InteractionTypes;
    public static readonly Manager<InnateItemPropertyType> InnateItemPropertyTypes;
    public static readonly Manager<PropertyActionType> PropertyActionTypes;

    static ItemManager()
    {
        ItemTypes = new Manager<ItemType>();
        ItemTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");

        AttributeTypes = new Manager<AttributeType>();
        AttributeTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyAttributeTypes.json");

        PropertyTypes = new Manager<PropertyType>();
        PropertyTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyTypes.json");

        InteractionTypes = new Manager<InteractionType>();
        InteractionTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");

        InnateItemPropertyTypes = new Manager<InnateItemPropertyType>();
        InnateItemPropertyTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../InnateItemProperties.json");

        PropertyActionTypes = new Manager<PropertyActionType>();
        PropertyActionTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../PropertyActionTypes.json");
    }
}
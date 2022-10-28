namespace ItemSystem;

public class GameManager
{
    public readonly Manager<ItemType> ItemTypes;
    public readonly Manager<ItemProperty> ItemProperties;
    public readonly Manager<ItemInteractionType> ItemInteractionTypes;

    public GameManager()
    {
        ItemTypes = new Manager<ItemType>();
        ItemTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");

        ItemProperties = new Manager<ItemProperty>();
        ItemProperties.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemProperties.json");

        ItemInteractionTypes = new Manager<ItemInteractionType>();
        ItemInteractionTypes.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");
    }
}
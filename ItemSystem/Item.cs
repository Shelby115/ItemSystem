namespace ItemSystem;

public class Item
{
    public ItemType Type { get; }
    public ICollection<ItemProperty> Properties { get; }
    public string Description
    {
        get
        {
            var additionalDescriptions = String.Join(" ", Properties.Select(x => x.Description));
            return $"{Type.Description} {additionalDescriptions}".Sentence();
        }
    }

    public Item(ItemType type)
    {
        Type = type;
        Properties = new List<ItemProperty>();
    }

    public override string ToString()
    {
        return $"{Type.Name}, {Description}".Sentence();
    }
}
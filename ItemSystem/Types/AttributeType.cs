namespace ItemSystem.Types;

/// <summary>
/// An attribute type is simply a name and description for an item property attribute.
/// For example, the attribute type "Number of uses" will be the same attribute type regardless if it is applied to the "Poisoned" or "Shiny" item properties.
/// </summary>
public class AttributeType
{
    public string Name { get; }
    public string Description { get; }
    public bool WillValueDecreaseOnUse { get; }
    public bool IsRemovedWhenValueReachesZero { get; }

    public AttributeType(string name, string description, bool willValueDecreaseOnUse, bool isRemovedWhenValueReachesZero)
    {
        Name = name;
        Description = description;
        WillValueDecreaseOnUse = willValueDecreaseOnUse;
        IsRemovedWhenValueReachesZero = isRemovedWhenValueReachesZero;
    }

    public override string ToString()
    {
        return $"{Name}, {Description}".Sentence();
    }
}
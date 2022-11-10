using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="ItemPropertyType"/> with a set of attributes associated.
/// An example would be an item property type of "Poisoned" with attributes of "Number of Uses" and "Added Poison Damage" associated.
/// </summary>
public class ItemProperty
{
    public ItemPropertyType PropertyType { get; }
    public ICollection<ItemPropertyAttribute> Attributes { get; }

    public ItemProperty(ItemPropertyType itemPropertyType)
    {
        PropertyType = itemPropertyType;
        Attributes = PropertyType
            .AttributeTypes
            .Select(x => new ItemPropertyAttribute(ItemManager.AttributeTypes.First(at => at.Name == x.Name), x.DefaultValue))
            .ToList();
    }

    public override string ToString()
    {
        var attributesDescription = $" {String.Join(" ", Attributes.Select(x => "[" + x + "]"))}";
        return $"{PropertyType.Name}{attributesDescription}".Trim();
    }
}
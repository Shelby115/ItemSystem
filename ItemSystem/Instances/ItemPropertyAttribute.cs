using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="ItemPropertyAttributeType"/> with a value associated.
/// An example would be an attribute type of "Number of Uses" and a value of 1 or an attribute type of "Added poison damage" and a value of 10.
/// </summary>
public class ItemPropertyAttribute
{
    public ItemPropertyAttributeType AttributeType { get; }
    public int Value { get; set; }

    public ItemPropertyAttribute(ItemPropertyAttributeType attributeType, int value)
    {
        AttributeType = attributeType;
        Value = value;
    }
}
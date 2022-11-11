using ItemSystem.Events;
using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="ItemPropertyAttributeType"/> with a value associated.
/// An example would be an attribute type of "Number of Uses" and a value of 1 or an attribute type of "Added poison damage" and a value of 10.
/// </summary>
public class ItemPropertyAttribute
{
    private readonly ItemProperty ItemProperty;

    public ItemPropertyAttributeType AttributeType { get; }
    public AttributeValue AttributeValue { get; private set; }

    public event EventHandler<ItemPropertyAttributeExpiredEventArgs>? AttributeExpired;

    public ItemPropertyAttribute(ItemProperty itemProperty, ItemPropertyAttributeType attributeType, int value)
    {
        ItemProperty = itemProperty;

        AttributeType = attributeType;
        AttributeValue = new AttributeValue(value);

        ItemProperty.ItemUsed += ItemProperty_ItemUsed;
        AttributeValue.HasChanged += Value_HasChanged;
    }

    ~ItemPropertyAttribute()
    {
        ItemProperty.ItemUsed -= ItemProperty_ItemUsed;
        AttributeValue.HasChanged -= Value_HasChanged;
    }

    /// <summary>
    /// Checks if the value should expire, when the attribute's value changes.
    /// </summary>
    private void Value_HasChanged(object? sender, EventArgs e)
    {
        if (AttributeValue == 0 && AttributeType.IsRemovedWhenValueReachesZero)
        {
            AttributeExpired?.Invoke(this, new ItemPropertyAttributeExpiredEventArgs(this));
        }
    }

    /// <summary>
    /// Signals to the attribute that the associated item was used.
    /// </summary>
    private void ItemProperty_ItemUsed(object? sender, ItemEventArgs e)
    {
        if (AttributeType.WillValueDecreaseOnUse)
        {
            AttributeValue.Value -= 1;
        }
    }

    public override string ToString()
    {
        return $"{AttributeType.Name}: {AttributeValue}";
    }
}
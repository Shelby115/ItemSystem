using ItemSystem.Events;
using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="Types.AttributeType"/> with a value associated.
/// An example would be an attribute type of "Number of Uses" and a value of 1 or an attribute type of "Added poison damage" and a value of 10.
/// </summary>
public class PropertyAttribute
{
    private readonly Property ItemProperty;

    public AttributeType Type { get; }
    public LoudInteger AttributeValue { get; private set; }

    public event EventHandler<AttributeExpiredEventArgs>? AttributeExpired;

    public PropertyAttribute(Property itemProperty, AttributeType attributeType, int value)
    {
        ItemProperty = itemProperty;

        Type = attributeType;
        AttributeValue = new LoudInteger(value);

        ItemProperty.ItemUsed += ItemProperty_ItemUsed;
        AttributeValue.HasChanged += Value_HasChanged;
    }

    ~PropertyAttribute()
    {
        ItemProperty.ItemUsed -= ItemProperty_ItemUsed;
        AttributeValue.HasChanged -= Value_HasChanged;
    }

    /// <summary>
    /// Checks if the value should expire, when the attribute's value changes.
    /// </summary>
    private void Value_HasChanged(object? sender, EventArgs e)
    {
        if (AttributeValue == 0 && Type.IsRemovedWhenValueReachesZero)
        {
            AttributeExpired?.Invoke(this, new AttributeExpiredEventArgs(this));
        }
    }

    /// <summary>
    /// Signals to the attribute that the associated item was used.
    /// </summary>
    private void ItemProperty_ItemUsed(object? sender, ItemEventArgs e)
    {
        if (Type.WillValueDecreaseOnUse)
        {
            AttributeValue.Value -= 1;
        }
    }

    public override string ToString()
    {
        return $"{Type.Name}: {AttributeValue}";
    }
}
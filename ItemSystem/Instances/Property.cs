using ItemSystem.Collections;
using ItemSystem.Events;
using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="Types.PropertyType"/> with a set of attributes associated.
/// An example would be an item property type of "Poisoned" with attributes of "Number of Uses" and "Added Poison Damage" associated.
/// </summary>
public class Property
{
    private readonly Item Item;

    public PropertyType Type { get; }
    public Attributes Attributes { get; }

    public event EventHandler<ItemEventArgs>? ItemUsed;
    public event EventHandler<PropertyExpiredEventArgs>? HasExpired;

    public Property(Item item, PropertyType itemPropertyType)
    {
        Item = item;

        Type = itemPropertyType;
        Attributes = new Attributes(this);

        Item.Used += Item_ItemUsed;
        Attributes.CriticalAttributeExpired += Attributes_CriticalAttributeExpired;
    }

    ~Property()
    {
        Item.Used -= Item_ItemUsed;
    }

    /// <summary>
    /// Forwards the signal from the associated <see cref="Item"/> that it has been used.
    /// </summary>
    private void Item_ItemUsed(object? sender, ItemEventArgs e)
    {
        if (sender == e.Item) // Don't trigger an "ItemUsed" event for attributes if it was used with another item.
        {
            ItemUsed?.Invoke(this, e);
        }
    }

    /// <summary>
    /// Signals to the item property that one of its critical attributes has expired and it should also expire.
    /// </summary>
    private void Attributes_CriticalAttributeExpired(object? sender, EventArgs e)
    {
        HasExpired?.Invoke(this, new PropertyExpiredEventArgs(this));
    }

    public override string ToString()
    {
        var attributesDescription = $" {String.Join(" ", Attributes.Select(x => "[" + x + "]"))}";
        return $"{Type.Name}{attributesDescription}".Trim();
    }
}
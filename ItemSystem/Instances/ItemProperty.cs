using ItemSystem.Collections;
using ItemSystem.Events;
using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="ItemPropertyType"/> with a set of attributes associated.
/// An example would be an item property type of "Poisoned" with attributes of "Number of Uses" and "Added Poison Damage" associated.
/// </summary>
public class ItemProperty
{
    private readonly Item Item;

    public ItemPropertyType PropertyType { get; }
    public ItemPropertyAttributes Attributes { get; }

    public event EventHandler<ItemEventArgs>? ItemUsed;
    public event EventHandler<ItemPropertyExpiredEventArgs>? HasExpired;

    public ItemProperty(Item item, ItemPropertyType itemPropertyType)
    {
        Item = item;

        PropertyType = itemPropertyType;
        Attributes = new ItemPropertyAttributes(this);

        Item.ItemUsed += Item_ItemUsed;
        Attributes.CriticalAttributeExpired += Attributes_CriticalAttributeExpired;
    }

    ~ItemProperty()
    {
        Item.ItemUsed -= Item_ItemUsed;
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
        HasExpired?.Invoke(this, new ItemPropertyExpiredEventArgs(this));
    }

    public override string ToString()
    {
        var attributesDescription = $" {String.Join(" ", Attributes.Select(x => "[" + x + "]"))}";
        return $"{PropertyType.Name}{attributesDescription}".Trim();
    }
}
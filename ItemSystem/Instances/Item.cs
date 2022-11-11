using ItemSystem.Collections;
using ItemSystem.Events;
using ItemSystem.Types;

namespace ItemSystem.Instances;

/// <summary>
/// An instance of an <see cref="ItemType"/> with item properties associated.
/// An example would be an item type of Dagger with the properties "Connected to Rope" and "Poisoned" associated.
/// </summary>
public class Item
{
    public ItemType Type { get; }
    public ItemProperties Properties { get; }
    public string Description
    {
        get
        {
            var additionalDescriptions = string.Join(" ", Properties.Select(x => x.PropertyType.Description));
            return $"{Type.Description} {additionalDescriptions}".Sentence();
        }
    }

    public event EventHandler<ItemEventArgs>? ItemUsed;
    protected virtual void OnThisItemUsed() => ItemUsed?.Invoke(this, new ItemEventArgs(this));
    protected virtual void OnItemUsedWithAnother(Item otherItem) => ItemUsed?.Invoke(this, new ItemEventArgs(otherItem));

    public Item(ItemType itemType)
    {
        Type = itemType;
        Properties = new ItemProperties();
    }

    /// <summary>
    /// Uses this item (e.g., Drink potion, eat food, attack with weapon, etc).
    /// </summary>
    public virtual void Use()
    {
        // Announce that this item has been used.
        OnThisItemUsed();
    }

    /// <summary>
    /// Uses this item with another item in hopes of adding a property to this item through a special interaction (e.g., Polished Dagger, Poisoned Spear, etc).
    /// </summary>
    /// <param name="item">The target item to interact with this one.</param>
    public void UseWith(Item item)
    {
        // See if it is a valid item interaction.
        var suggestedInteraction = ItemManager.InteractionTypes.FirstOrDefault(x => x.SourceItem == Type.Name && x.TargetItem == item.Type.Name);
        if (suggestedInteraction == null) { return; }

        // Find the resulting item property.
        var addedProperty = ItemManager.PropertyTypes.FirstOrDefault(x => x.Name == suggestedInteraction.AddedProperty);
        if (addedProperty == null) { return; }

        // Check if it already exists, if it does remove it before adding it again.
        var sourceItemProperty = Properties.FirstOrDefault(x => x.PropertyType.Name == suggestedInteraction.AddedProperty);
        if (sourceItemProperty != null)
        {
            Properties.Remove(sourceItemProperty);
        }

        // Add the property from the interaction.
        Properties.Add(new ItemProperty(this, addedProperty));

        // Announce that this item has been used with another.
        OnItemUsedWithAnother(item);
    }

    public override string ToString()
    {
        return $"{Type.Name}, {this.Description}".Sentence();
    }
}
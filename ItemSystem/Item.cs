﻿namespace ItemSystem;

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

    public Item(string itemTypeName)
    {
        Type = ItemManager.Types.First(x => x.Name == itemTypeName);
        Properties = new List<ItemProperty>();
    }

    public Item(ItemType itemType)
    {
        Type = itemType;
        Properties = new List<ItemProperty>();
    }

    public void UseWith(Item item)
    {
        // See if it is a valid item interaction.
        var suggestedInteraction = ItemManager.InteractionTypes.FirstOrDefault(x => x.SourceItem == this.Type.Name && x.TargetItem == item.Type.Name);
        if (suggestedInteraction == null) { return; }

        // Find the resulting item property.
        var addedProperty = ItemManager.Properties.FirstOrDefault(x => x.Name == suggestedInteraction.AddedProperty);
        if (addedProperty == null) { return; }

        // Check if it already exists, if it does remove it before adding it again.
        var sourceItemProperty = this.Properties.FirstOrDefault(x => x.Name == suggestedInteraction.AddedProperty);
        if (sourceItemProperty != null)
        {
            this.Properties.Remove(sourceItemProperty);
        }

        // Add the property from the interaction.
        this.Properties.Add(addedProperty);
    }

    public override string ToString()
    {
        return $"{Type.Name}, {Description}".Sentence();
    }
}
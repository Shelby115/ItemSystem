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
    public Properties Properties { get; }
    public string Description
    {
        get
        {
            var additionalDescriptions = string.Join(" ", Properties.Select(x => x.Type.Description));
            return $"{Type.Description} {additionalDescriptions}".Sentence();
        }
    }

    public event EventHandler<ItemEventArgs>? Used;
    protected virtual void OnThisItemUsed() => Used?.Invoke(this, new ItemEventArgs(this));
    protected virtual void OnItemUsedWithAnother(Item otherItem) => Used?.Invoke(this, new ItemEventArgs(otherItem));

    public Item(ItemType type)
    {
        Type = type;
        Properties = new Properties();
        AddInnateProperties();
    }

    /// <summary>
    /// Adds the properties innate to this item type (e.g., Daggers might have the "Weapon" property type).
    /// </summary>
    private void AddInnateProperties()
    {
        var innatePropertyTypes = ItemManager.InnateItemPropertyTypes.Where(x => x.ItemType == this.Type.Name);
        foreach (var innatePropertyType in innatePropertyTypes)
        {
            Properties.Add(new Property(this, ItemManager.PropertyTypes.First(x => x.Name == innatePropertyType.PropertyType)));
        }
    }

    /// <summary>
    /// Retrieves a list of action types that should be available to the item.
    /// </summary>
    private IEnumerable<PropertyActionType> GetAvailableActionTypes()
    {
        var appliableActionTypes = ItemManager
            .PropertyActionTypes
            // Does this item have all of the required properties for the action? (e.g., Pull Rope would require both Thrown and Rope Connected).
            .Where(x => x.PropertyNames.All(pn => Properties.Any(p => p.Type.Name == pn)))
            // Does this item have none of the preventing properties for the action? (e.g., Attack would require that it is not Thrown).
            .Where(x => x.PreventingProperties.Any(pp => Properties.Any(p => p.Type.Name == pp)) == false);
        return appliableActionTypes;
    }

    /// <summary>
    /// Retrieves a list of action names available for the item based on its type and properties.
    /// </summary>
    public IEnumerable<string> GetAvailableActions()
    {
        return GetAvailableActionTypes().Select(x => x.ActionName);
    }

    /// <summary>
    /// Executes the action with the specified name (if found).
    /// </summary>
    /// <param name="actionName">Name of the action to be executed.</param>
    public void Act(string actionName)
    {
        var applicableActions = GetAvailableActionTypes()
            .Where(x => x.ActionName == actionName);
        var changes = new Dictionary<bool, Property>();

        foreach (var actionType in applicableActions)
        {
            if (actionType.RemovedProperty != null)
            {
                var property = Properties.FirstOrDefault(x => x.Type.Name == actionType.RemovedProperty);
                if (property != null)
                {
                    changes.Add(false, property);
                }
            }

            if (actionType.AddedProperty != null)
            {
                var existingProperty = Properties.FirstOrDefault(x => x.Type.Name == actionType.AddedProperty);
                var propertyType = ItemManager.PropertyTypes.FirstOrDefault(x => x.Name == actionType.AddedProperty);
                if (propertyType != null)
                {
                    var newProperty = new Property(this, propertyType);
                    if (newProperty != null)
                    {
                        changes.Add(true, newProperty);
                        if (existingProperty != null)
                        {
                            changes.Add(false, existingProperty);
                        }
                    }
                }
            }
        }

        foreach (var change in changes)
        {
            if (change.Key == true)
            {
                Properties.Add(change.Value);
            }
            else
            {
                Properties.Remove(change.Value);
            }
        }
    }

    /// <summary>
    /// Uses this item (e.g., Drink potion, eat food, attack with weapon, etc).
    /// </summary>
    public void Use()
    {
        // Announce that this item has been used.
        OnThisItemUsed();
    }

    /// <summary>
    /// Filters the list of interactions to those that only apply to both the source item and target item.
    /// </summary>
    private static IEnumerable<InteractionType> GetInteractions(IEnumerable<InteractionType> interactions, Item sourceItem, Item targetItem)
    {
        var sourceInteractions = interactions
            // Retrieve interactions where the item type matches or an item type is not required.
            .Where(x => x.SourceItem == null || x.SourceItem == sourceItem.Type.Name)
            // Retrieve interactions where the required source item properties match.
            .Where(x => x.SourceItemProperties.Any() == false || x.SourceItemProperties.All(sip => sourceItem.Properties.Any(p => p.Type.Name == sip)))
            // Retrieve interactions where there are no preventing item properties.
            .Where(x => x.SourceItemPreventProperties.Any() == false || x.SourceItemPreventProperties.Any(sipp => sourceItem.Properties.Any(p => p.Type.Name == sipp)) == false);

        // TL;DR - Repeat the same logic for the Target Item.
        return sourceInteractions
            // Retrieve interactions where the item type matches or an item type is not required.
            .Where(x => x.TargetItem == null || x.TargetItem == targetItem.Type.Name)
            // Retrieve interactions where the required source item properties match.
            .Where(x => x.TargetItemProperties.Any() == false || x.TargetItemProperties.All(sip => targetItem.Properties.Any(p => p.Type.Name == sip)))
            // Retrieve interactions where there are no preventing item properties.
            .Where(x => x.TargetItemPreventProperties.Any() == false || x.TargetItemPreventProperties.Any(sipp => targetItem.Properties.Any(p => p.Type.Name == sipp)) == false);
    }

    /// <summary>
    /// Uses this item with another item in hopes of adding a property to this item through a special interaction (e.g., Polished Dagger, Poisoned Spear, etc).
    /// </summary>
    /// <param name="item">The target item to interact with this one.</param>
    public void UseWith(Item item)
    {
        // Retrieve a list of valid interactions.
        var interactions = GetInteractions(ItemManager.InteractionTypes, this, item);
        foreach (var interaction in interactions)
        {
            if (interaction.AddedProperty != null)
            {
                // Find the resulting item property.
                var addedProperty = ItemManager.PropertyTypes.FirstOrDefault(x => x.Name == interaction.AddedProperty);
                if (addedProperty != null)
                {
                    // Check if it already exists, if it does remove it before adding it again.
                    var sourceItemProperty = Properties.FirstOrDefault(x => x.Type.Name == interaction.AddedProperty);
                    if (sourceItemProperty != null)
                    {
                        Properties.Remove(sourceItemProperty);
                    }

                    // Add the property from the interaction.
                    Properties.Add(new Property(this, addedProperty));
                }
            }

            if (interaction.RemovedProperty != null)
            {
                var propertyToRemove = Properties.FirstOrDefault(x => x.Type.Name == interaction.RemovedProperty);
                if (propertyToRemove != null)
                {
                    Properties.Remove(propertyToRemove);
                }
            }
        }

        // Announce that this item has been used with another.
        OnItemUsedWithAnother(item);
    }

    public override string ToString()
    {
        return $"{Type.Name}, {this.Description}".Sentence();
    }
}
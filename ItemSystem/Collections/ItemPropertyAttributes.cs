using ItemSystem.Events;
using ItemSystem.Instances;
using System.Collections;

namespace ItemSystem.Collections;

/// <summary>
/// A collection of attributes whose members are determined upon creation (based on the item property).
/// </summary>
public class ItemPropertyAttributes : IEnumerable<ItemPropertyAttribute>
{
    private readonly ItemProperty ItemProperty;
    private readonly ICollection<ItemPropertyAttribute> Attributes;

    public int Count => Attributes.Count;

    public event EventHandler? CriticalAttributeExpired;

    public ItemPropertyAttributes(ItemProperty itemProperty)
    {
        ItemProperty = itemProperty;
        Attributes = new List<ItemPropertyAttribute>();
        foreach (var attributeType in ItemProperty.PropertyType.AttributeTypes)
        {
            var attribute = new ItemPropertyAttribute(ItemProperty, ItemManager.AttributeTypes.First(x => x.Name == attributeType.Name), attributeType.DefaultValue);
            attribute.AttributeExpired += Attribute_AttributeExpired;
            Attributes.Add(attribute);
        }
    }

    /// <summary>
    /// Fires the <see cref="CriticalAttributeExpired"/> event when one of the attributes expires and the item property is configured to expire when one does.
    /// </summary>
    private void Attribute_AttributeExpired(object? sender, ItemPropertyAttributeExpiredEventArgs e)
    {
        if (ItemProperty.PropertyType.WillExpireWhenAnAttributeExpires)
        {
            CriticalAttributeExpired?.Invoke(this, EventArgs.Empty);
        }
    }

    #region IEnumerable<ItemPropertyAttribute> Implementation
    public IEnumerator<ItemPropertyAttribute> GetEnumerator()
    {
        return Attributes.GetEnumerator(); ;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Attributes.GetEnumerator(); ;
    }

    #endregion IEnumerable<ItemPropertyAttribute> Implementation
}
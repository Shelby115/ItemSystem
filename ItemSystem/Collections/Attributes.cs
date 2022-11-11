using ItemSystem.Events;
using ItemSystem.Instances;
using System.Collections;

namespace ItemSystem.Collections;

/// <summary>
/// A collection of attributes whose members are determined upon creation (based on the item property).
/// </summary>
public class Attributes : IEnumerable<PropertyAttribute>
{
    private readonly Property Property;
    private readonly ICollection<PropertyAttribute> _Attributes;

    public int Count => _Attributes.Count;

    public event EventHandler? CriticalAttributeExpired;

    public Attributes(Property itemProperty)
    {
        Property = itemProperty;
        _Attributes = new List<PropertyAttribute>();
        foreach (var attributeType in Property.Type.AttributeTypes)
        {
            var attribute = new PropertyAttribute(Property, ItemManager.AttributeTypes.First(x => x.Name == attributeType.Name), attributeType.DefaultValue);
            attribute.AttributeExpired += Attribute_AttributeExpired;
            _Attributes.Add(attribute);
        }
    }

    /// <summary>
    /// Fires the <see cref="CriticalAttributeExpired"/> event when one of the attributes expires and the item property is configured to expire when one does.
    /// </summary>
    private void Attribute_AttributeExpired(object? sender, AttributeExpiredEventArgs e)
    {
        if (Property.Type.WillExpireWhenAnAttributeExpires)
        {
            CriticalAttributeExpired?.Invoke(this, EventArgs.Empty);
        }
    }

    #region IEnumerable<PropertyAttribute> Implementation
    public IEnumerator<PropertyAttribute> GetEnumerator()
    {
        return _Attributes.GetEnumerator(); ;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Attributes.GetEnumerator(); ;
    }

    #endregion IEnumerable<PropertyAttribute> Implementation
}
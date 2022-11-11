using ItemSystem.Instances;
using System.Collections;

namespace ItemSystem.Collections;

/// <summary>
/// A collection of item properties.
/// </summary>
public class ItemProperties : IEnumerable<ItemProperty>
{
    private readonly ICollection<ItemProperty> Properties;

    public int Count => Properties.Count;

    public ItemProperties()
    {
        Properties = new List<ItemProperty>();
    }

    public void Add(ItemProperty property)
    {
        Properties.Add(property);
    }

    public void Remove(ItemProperty property)
    {
        Properties.Remove(property);
    }

    #region IEnumerable<ItemProperty> Implementation

    public IEnumerator<ItemProperty> GetEnumerator()
    {
        return Properties.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Properties.GetEnumerator();
    }

    #endregion  IEnumerable<ItemProperty> Implementation
}
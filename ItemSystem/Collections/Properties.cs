using ItemSystem.Instances;
using System.Collections;

namespace ItemSystem.Collections;

/// <summary>
/// A collection of item properties.
/// </summary>
public class Properties : IEnumerable<Property>
{
    private readonly ICollection<Property> _Properties;

    public int Count => _Properties.Count;

    public Properties()
    {
        _Properties = new List<Property>();
    }

    public void Add(Property property)
    {
        _Properties.Add(property);
    }

    public void Remove(Property property)
    {
        _Properties.Remove(property);
    }

    #region IEnumerable<Property> Implementation

    public IEnumerator<Property> GetEnumerator()
    {
        return _Properties.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _Properties.GetEnumerator();
    }

    #endregion  IEnumerable<Property> Implementation
}
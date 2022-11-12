namespace ItemSystem.Types;

/// <summary>
/// A named action for an item that can be used when the defined property is on the item.
/// For exmaple, the Weapon property might add Attack or the Rope Connected property might add Untie.
/// </summary>
public class PropertyActionType
{
    public string[] PropertyNames { get; }
    public string ActionName { get; }
    public string RemovedProperty { get; }
    public string AddedProperty { get; }

    public PropertyActionType(string[] propertyNames, string actionName, string removedProperty, string addedProperty)
    {
        PropertyNames = propertyNames;
        ActionName = actionName;
        RemovedProperty = removedProperty;
        AddedProperty = addedProperty;
    }
}
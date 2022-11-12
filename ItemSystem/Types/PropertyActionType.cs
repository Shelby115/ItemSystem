namespace ItemSystem.Types;

/// <summary>
/// A named action for an item that can be used when the defined property is on the item.
/// For exmaple, the Weapon property might add Attack or the Rope Connected property might add Untie.
/// </summary>
public class PropertyActionType
{
    public string ActionName { get; }
    public string[] PropertyNames { get; }
    public string[] PreventingProperties { get; }
    public string RemovedProperty { get; }
    public string AddedProperty { get; }

    public PropertyActionType(string actionName, string[] propertyNames, string[] preventingProperties, string removedProperty, string addedProperty)
    {
        ActionName = actionName;
        PropertyNames = propertyNames ?? new string[0];
        PreventingProperties = preventingProperties ?? new string[0];
        RemovedProperty = removedProperty;
        AddedProperty = addedProperty;
    }
}
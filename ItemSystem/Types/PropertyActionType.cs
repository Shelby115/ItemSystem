namespace ItemSystem.Types;

/// <summary>
/// A named action for an item that can be used when the defined property is on the item.
/// For exmaple, the Weapon property might add Attack or the Rope Connected property might add Untie.
/// </summary>
public class PropertyActionType
{
    public string PropertyName { get; }
    public string ActionName { get; }
    public bool WillRemovePropertyOnAction { get; }

    public PropertyActionType(string propertyName, string actionName, bool willRemovePropertyOnAction)
    {
        PropertyName = propertyName;
        ActionName = actionName;
        WillRemovePropertyOnAction = willRemovePropertyOnAction;
    }
}
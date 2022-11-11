namespace ItemSystem.Types;

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
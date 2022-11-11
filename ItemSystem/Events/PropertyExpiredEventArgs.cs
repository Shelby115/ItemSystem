using ItemSystem.Instances;

namespace ItemSystem.Events;

public class PropertyExpiredEventArgs : EventArgs
{
    public Property ItemProperty { get; }
    
    public PropertyExpiredEventArgs(Property itemProperty)
    {
        ItemProperty = itemProperty;
    }
}
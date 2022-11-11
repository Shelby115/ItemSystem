using ItemSystem.Instances;

namespace ItemSystem.Events;

public class ItemPropertyAttributeExpiredEventArgs : EventArgs
{
    public ItemPropertyAttribute Attribute { get; }

    public ItemPropertyAttributeExpiredEventArgs(ItemPropertyAttribute attribute)
    {
        Attribute = attribute;
    }
}
using ItemSystem.Instances;

namespace ItemSystem.Events;

public class AttributeExpiredEventArgs : EventArgs
{
    public Instances.PropertyAttribute Attribute { get; }

    public AttributeExpiredEventArgs(Instances.PropertyAttribute attribute)
    {
        Attribute = attribute;
    }
}
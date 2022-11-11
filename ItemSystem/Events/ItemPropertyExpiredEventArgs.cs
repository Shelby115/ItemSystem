using ItemSystem.Instances;

namespace ItemSystem.Events;

public class ItemPropertyExpiredEventArgs : EventArgs
{
    public ItemProperty ItemProperty { get; }
    
    public ItemPropertyExpiredEventArgs(ItemProperty itemProperty)
    {
        ItemProperty = itemProperty;
    }
}
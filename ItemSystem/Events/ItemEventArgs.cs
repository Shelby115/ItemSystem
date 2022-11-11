using ItemSystem.Instances;

namespace ItemSystem.Events;

public class ItemEventArgs : EventArgs
{
    public Item Item { get; }

    public ItemEventArgs(Item item)
    {
        Item = item;
    }
}
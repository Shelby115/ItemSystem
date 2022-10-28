namespace ItemSystem;

public class ItemInteractionType
{
    public string SourceItem { get; }
    public string TargetItem { get; }
    public string AddedProperty { get; }

    public ItemInteractionType(string sourceItem, string targetItem, string addedProperty)
    {
        SourceItem = sourceItem;
        TargetItem = targetItem;
        AddedProperty = addedProperty;
    }

    public override string ToString()
    {
        return $"{SourceItem} + {TargetItem} = {AddedProperty}";
    }
}
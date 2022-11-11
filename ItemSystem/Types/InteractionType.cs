namespace ItemSystem.Types;

public class InteractionType
{
    public string SourceItem { get; }
    public string TargetItem { get; }
    public string AddedProperty { get; }
    public string RemovedProperty { get; }

    public InteractionType(string sourceItem, string targetItem, string addedProperty, string removedProperty)
    {
        SourceItem = sourceItem;
        TargetItem = targetItem;
        AddedProperty = addedProperty;
        RemovedProperty = removedProperty;
    }

    public override string ToString()
    {
        return $"{SourceItem} + {TargetItem} = {AddedProperty}-{RemovedProperty}";
    }
}
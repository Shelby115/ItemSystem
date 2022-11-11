namespace ItemSystem.Types;

/// <summary>
/// A defined interaction between a source and target items. Can add or remove a property.
/// </summary>
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
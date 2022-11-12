namespace ItemSystem.Types;

/// <summary>
/// A defined interaction between a source and target items. Can add or remove a property.
/// </summary>
public class InteractionType
{
    public string SourceItem { get; }
    public string[] SourceItemProperties { get; }
    public string[] SourceItemPreventProperties { get; }
    public string TargetItem { get; }
    public string[] TargetItemProperties { get; }
    public string[] TargetItemPreventProperties { get; }
    public string AddedProperty { get; }
    public string RemovedProperty { get; }

    public InteractionType(string sourceItem, string[] sourceItemProperties, string[] sourceItemPreventProperties, string targetItem, string[] targetItemProperties, string[] targetItemPreventProperties, string addedProperty, string removedProperty)
    {
        SourceItem = sourceItem;
        SourceItemProperties = sourceItemProperties ?? new string[0];
        SourceItemPreventProperties = sourceItemPreventProperties ?? new string[0];
        TargetItem = targetItem;
        TargetItemProperties = targetItemProperties ?? new string[0];
        TargetItemPreventProperties = targetItemPreventProperties ?? new string[0];
        AddedProperty = addedProperty;
        RemovedProperty = removedProperty;
    }

    public override string ToString()
    {
        return $"{SourceItem} + {TargetItem} = {AddedProperty}-{RemovedProperty}";
    }
}
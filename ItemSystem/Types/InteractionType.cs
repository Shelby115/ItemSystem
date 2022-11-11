namespace ItemSystem.Types;

public class InteractionType
{
    public string SourceItem { get; }
    public string TargetItem { get; }
    public string AddedProperty { get; }

    public InteractionType(string sourceItem, string targetItem, string addedProperty)
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
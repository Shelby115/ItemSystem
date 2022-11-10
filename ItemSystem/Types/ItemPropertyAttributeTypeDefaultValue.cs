namespace ItemSystem.Types;

public class ItemPropertyAttributeTypeDefaultValue
{
    public string Name { get; }
    public int DefaultValue { get; }

    public ItemPropertyAttributeTypeDefaultValue(string name, int defaultValue)
    {
        Name = name;
        DefaultValue = defaultValue;
    }
}
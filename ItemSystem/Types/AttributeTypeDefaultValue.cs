namespace ItemSystem.Types;

public class AttributeTypeDefaultValue
{
    public string Name { get; }
    public int DefaultValue { get; }

    public AttributeTypeDefaultValue(string name, int defaultValue)
    {
        Name = name;
        DefaultValue = defaultValue;
    }

    public override string ToString()
    {
        return $"{Name} - {DefaultValue}";
    }
}
namespace ItemSystem;

public class AttributeValue
{
    public event EventHandler? HasChanged;

    private int _Value;
    public int Value
    {
        get => _Value;
        set
        {
            _Value = value;
            HasChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public AttributeValue(int value = 0)
    {
        _Value = value;
    }

    public static implicit operator int(AttributeValue value) => value._Value;

    public override string ToString()
    {
        return _Value.ToString();
    }
}
namespace ItemSystem;

/// <summary>
/// An integer value that announces when it changes.
/// </summary>
public class LoudInteger
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

    public LoudInteger(int value = 0)
    {
        _Value = value;
    }

    public static implicit operator int(LoudInteger value) => value._Value;

    public override string ToString()
    {
        return _Value.ToString();
    }
}
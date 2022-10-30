namespace ItemSystem;

/// <summary>
/// An item property attribute value can be thought of as a "current value" for the item property attribute.
/// For example, let's imagine the following scenario.
/// There exists an item property named "Poisoned" that has an attribute type of "Number of uses" and it defaults to a value of 2.
/// All of the above information is purely provided by the ItemPropertyAttribute class and its properties and sub-classes.
/// Now, if we were to use this item, it would no longer have a "Number of uses" of 2, it would now be 1.
/// This is the class where we track that information. The <see cref="Value"/> property on this class will be updated to reflect that.
/// </summary>
public class ItemPropertyAttributeValue
{
    public ItemPropertyAttribute Attribute { get; }
    public int Value { get; set; }

    public ItemPropertyAttributeValue(string attributeType, string itemProperty, int? value = null)
    {
        Attribute = ItemManager.Attributes.First(x => x.AttributeType.Name == attributeType && x.ItemProperty.Name == itemProperty);
        Value = value ?? Attribute.DefaultValue;
    }
}
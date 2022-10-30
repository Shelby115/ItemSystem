using ItemSystem.JsonConverters;
using System.Text.Json.Serialization;

namespace ItemSystem
{
    /// <summary>
    /// An item property attribute is the combination of an item property (e.g., Poisoned or Shiny) and an attribute type (e.g., Number of uses).
    /// The combination of these two things comes with a default value. This is a separate class because you might want a different default value
    /// for the same attribute type on different item properties (e.g., Poisoned might be 2 uses and Shiny might be 5 uses).
    /// </summary>
    [JsonConverter(typeof(ItemPropertyAttributeJsonConverter))]
    public class ItemPropertyAttribute
    {
        public ItemPropertyAttributeType AttributeType { get; }
        public ItemProperty ItemProperty { get; }
        public int DefaultValue { get; }

        public ItemPropertyAttribute(string attributeType, string itemProperty, int defaultValue)
        {
            AttributeType = ItemManager.AttributeTypes.First(x => x.Name == attributeType);
            ItemProperty = ItemManager.Properties.First(x => x.Name == itemProperty);
            DefaultValue = defaultValue;
        }

        public override string ToString()
        {
            return $"{ItemProperty} has a default value of {DefaultValue} {AttributeType}".Sentence();
        }
    }
}
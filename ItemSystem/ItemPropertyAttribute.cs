namespace ItemSystem
{
    public class ItemPropertyAttribute<T>
    {
        public ItemPropertyAttributeType AttributeType { get; set; }
        public T Value { get; set; }

        public ItemPropertyAttribute(ItemPropertyAttributeType attributeType, T value)
        {
            AttributeType = attributeType;
            Value = value;
        }

        public override string ToString()
        {
            return $"{AttributeType} which has a value of {Value}".Sentence();
        }
    }
}
namespace ItemSystem
{
    public class ItemPropertyAttributeType
    {
        public string Name { get; }
        public string Description { get; }

        public ItemPropertyAttributeType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name}, {Description}".Sentence();
        }
    }
}
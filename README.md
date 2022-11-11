# Item System

## Type Entities
Type entities shall be loaded from configuration files and will be immutable once loaded. These define types like "Dagger", "Shiny", or "Number of Uses" and not the values associated with the types.

1. ItemType - A name and description for an item (e.g., Dagger, Spear, Rope, Poison Vial, etc).
2. PropertyType - A name and description for a property of an item (e.g., Shiny, Worn, Connected to Rope, Poisoned, etc).
3. InteractionType - A set of two items that when used together add an item property to the source item (e.g., A dagger used with a poison vial will add the poisoned property).
4. AtttributeType - A name and description for an attribute of a property (e.g., The poisoned property might have the attributes "Number of uses" and "Added poison damage").
5. AttributeTypeDefaultValue - An attribute type name, property type name, and a default value.

## Instance Entities
These entities are instances of type entities. They generally have one or more types associated with them as well as a value attached which is mutable as opposed to the immutable type entities.

1. ropertyAttribute - An instance of an ItemPropertyAttributeType with a value associated.
2. Property - An instance of an ItemPropertyType with a set of attributes associated.
3. Item - An instance of an ItemType with properties associated.


## TODO

1. ~~Add a method for easily updating the value of a property attribute (Events maybe?).~~
2. ~~Add a method for easily removing a property from an item (e.g., Number of uses reaches 0, Rope is cut or removed, Dagger is bloodied, etc).~~
3. Ability to require a property for an interaction.
4. Ability to remove a property via an interaction.
5. Ability to require only a property for an interaction.
6. List and Add actions based on an item's properties.
7. ~~Add innate item properties for item types.~~

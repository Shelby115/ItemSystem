# Item System

## Type Entities
Type entities shall be loaded from configuration files and will be immutable once loaded. These define types like "Dagger", "Shiny", or "Number of Uses" and not the values associated with the types.

1. ItemType - A name and description for an item (e.g., Dagger, Spear, Rope, Poison Vial, etc).
2. ItemPropertyType - A name and description for a property of an item (e.g., Shiny, Worn, Connected to Rope, Poisoned, etc).
3. ItemInteractionType - A set of two items that when used together add an item property to the source item (e.g., A dagger used with a poison vial will add the poisoned property).
4. ItemPropertyAtttributeType - A name and description for an attribute of a property (e.g., The poisoned property might have the attributes "Number of uses" and "Added poison damage").

## Instance Entities
These entities are instances of type entities. They generally have one or more types associated with them as well as a value attached which is mutable as opposed to the immutable type entities.

1. ItemPropertyAttribute - An instance of an ItemPropertyAttributeType with a value associated.
2. ItemProperty - An instance of an ItemPropertyType with a set of attributes associated.
3. Item - An instance of an ItemType with properties associated.

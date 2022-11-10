# Item System

## Type Entities

1. ItemType - A name and description for an item (e.g., Dagger, Spear, Rope, Poison Vial, etc).
2. ItemPropertyType - A name and description for a property of an item (e.g., Shiny, Worn, Connected to Rope, Poisoned, etc).
3. ItemInteractionType - A set of two items that when used together add an item property to the source item (e.g., A dagger used with a poison vial will add the poisoned property).
4. ItemPropertyAtttributeType - A name and description for an attribute of a property (e.g., The poisoned property might have the attributes "Number of uses" and "Added poison damage").

## Instance Entities

1. ItemPropertyAttribute - An instance of an ItemPropertyAttributeType with a value associated.
2. ItemProperty - An instance of an ItemPropertyType with a set of attributes associated.
3. Item - An instance of an ItemType with properties associated.

using ItemSystem.Events;
using ItemSystem.Instances;
using ItemSystem.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class ItemTests
{
    [TestMethod]
    public void Item_Constructor()
    {
        var item = new Item(new ItemType("Test", "test"));
        Assert.IsNotNull(item);
        Assert.IsNotNull(item.Type);
    }

    [TestMethod]
    public void Item_ItemProperty_Descriptions()
    {
        var item = new Item(new ItemType("Test", "test"));

        Assert.IsNotNull(item);
        Assert.IsNotNull(item.Type);
        Assert.AreEqual<string>("test", item.Type.Description);
        Assert.AreEqual<string>("Test.", item.Description);

        var propertyType = new ItemPropertyType("Rope", "connected to a rope", false, new List<ItemPropertyAttributeTypeDefaultValue>());
        item.Properties.Add(new ItemProperty(item, propertyType));

        Assert.IsNotNull(item.Properties);
        Assert.AreEqual<string>("Test connected to a rope.", item.Description);
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenInteractionNotFound()
    {
        var game = new ItemManager();
        var sourceItem = new Item(new ItemType("A", "a"));
        var targetItem = new Item(new ItemType("B", "b"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(targetItem);
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenItemPropertyNotFound()
    {
        var game = new ItemManager();
        var sourceItem = new Item(new ItemType("B", "b"));
        var targetItem = new Item(new ItemType("C", "c"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(targetItem);
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
    }

    [TestMethod]
    public void Item_UseWith_AddsProperty_WhenInteractionAndPropertyFound()
    {
        var sourceItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var targetItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Rope"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(targetItem);
        Assert.AreEqual<int>(1, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        var property = sourceItem.Properties.FirstOrDefault(x => x.PropertyType.Name == "Rope Connected");
        Assert.IsNotNull(property);
    }

    [TestMethod]
    public void Item_UseWith_AddsPropertyWithValue_WhenInteractionAndPropertyFound()
    {
        var sourceItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var targetItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count, "Starting source item property count.");
        Assert.AreEqual<int>(0, targetItem.Properties.Count, "Starting target item property count.");
        sourceItem.UseWith(targetItem);
        Assert.AreEqual<int>(1, sourceItem.Properties.Count, "Source item property count.");
        Assert.AreEqual<int>(0, targetItem.Properties.Count, "Target item property count.");
        var property = sourceItem.Properties.FirstOrDefault(x => x.PropertyType.Name == "Poisoned");
        Assert.IsNotNull(property);
        Assert.IsNotNull(property.Attributes, "Property attributes.");
        Assert.AreEqual<int>(2, property.Attributes.Count, "Property attribute count.");
        var numberOfUses = property.Attributes.FirstOrDefault(x => x.AttributeType.Name == "Number of Uses");
        Assert.IsNotNull(numberOfUses);
        Assert.AreEqual<int>(1, numberOfUses.AttributeValue, "Number of uses value.");
        var addedPoisonDamage = property.Attributes.FirstOrDefault(x => x.AttributeType.Name == "Added Poison Damage");
        Assert.IsNotNull(addedPoisonDamage);
        Assert.AreEqual<int>(10, addedPoisonDamage.AttributeValue, "Added poison damage value.");
    }

    [TestMethod]
    public void Item_Use_TriggersItemUsedEvent()
    {
        var eventsTriggered = new List<ItemEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        item.ItemUsed += delegate (object? sender, ItemEventArgs e)
        {
            eventsTriggered.Add(e);
        };

        item.Use();
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
        Assert.AreEqual<Item>(item, eventsTriggered.First().Item);
    }

    [TestMethod]
    public void Item_Use_TriggersItemPropertyItemUsedEvent()
    {
        var eventsTriggered = new List<ItemEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);

        var itemProperty = item.Properties.First(x => x.PropertyType.Name == "Poisoned");
        itemProperty.ItemUsed += delegate (object? sender, ItemEventArgs e)
        {
            eventsTriggered.Add(e);
        };

        item.Use();
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
        Assert.AreEqual<Item>(item, eventsTriggered.First().Item);
    }

    [TestMethod]
    public void Item_Use_ReducesNumberOfUsesByOne()
    {
        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);
        var itemProperty = item.Properties.First(x => x.PropertyType.Name == "Poisoned");
        var attribute = itemProperty.Attributes.First(x => x.AttributeType.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.AttributeType.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
    }

    [TestMethod]
    public void Item_Use_ItemPropertyAttributeFiresExpiredEventWhenValueReachesZero()
    {
        var eventsTriggered = new List<ItemPropertyAttributeExpiredEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);
        var itemProperty = item.Properties.First(x => x.PropertyType.Name == "Poisoned");
        var attribute = itemProperty.Attributes.First(x => x.AttributeType.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.AttributeType.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        Assert.AreEqual<bool>(true, attribute.AttributeType.IsRemovedWhenValueReachesZero, "Check that 'Number of Uses' is configured to expire when zero.");
        attribute.AttributeExpired += delegate (object? sender, ItemPropertyAttributeExpiredEventArgs e)
        {
            eventsTriggered.Add(e);
        };
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
    }

    [TestMethod]
    public void Item_Use_ItemPropertyExpiresWhenAttributeExpires()
    {
        var eventsTriggered = new List<ItemPropertyExpiredEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);
        var itemProperty = item.Properties.First(x => x.PropertyType.Name == "Poisoned");
        Assert.AreEqual<bool>(true, itemProperty.PropertyType.WillExpireWhenAnAttributeExpires, "Check that 'Poisoned' is confiugred to expire when an attribute expires.");
        itemProperty.HasExpired += delegate (object? sender, ItemPropertyExpiredEventArgs e)
        {
            eventsTriggered.Add(e);
        };
        var attribute = itemProperty.Attributes.First(x => x.AttributeType.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.AttributeType.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        Assert.AreEqual<bool>(true, attribute.AttributeType.IsRemovedWhenValueReachesZero, "Check that 'Number of Uses' is configured to expire when zero.");
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
    }
}
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

        var propertyType = new PropertyType("Rope", "connected to a rope", false, new List<AttributeTypeDefaultValue>());
        item.Properties.Add(new Property(item, propertyType));

        Assert.IsNotNull(item.Properties);
        Assert.AreEqual<string>("Test connected to a rope.", item.Description);
    }

    [TestMethod]
    public void Item_Act_AttackCanBeUsedTwice()
    {
        var weapon = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        Assert.IsTrue(weapon.Properties.Any(x => x.Type.Name == "Weapon"), "Item has the weapon property.");
        var actions = weapon.GetAvailableActions();
        Assert.IsTrue(actions.Any(x => x == "Attack"), "Item has the attack action available.");
        weapon.Act("Attack");
        Assert.IsTrue(actions.Any(x => x == "Attack"), "The attack action is still available after attacking.");
        weapon.Act("Attack");
    }

    [TestMethod]
    public void Item_Act_UntieRemovesRopeConnectedProperty()
    {
        var weapon = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var rope = new Item(ItemManager.ItemTypes.First(x => x.Name == "Rope"));
        Assert.IsFalse(weapon.Properties.Any(x => x.Type.Name == "Rope Connected"), "Starts without rope connected.");
        weapon.UseWith(rope);
        Assert.IsTrue(weapon.Properties.Any(x => x.Type.Name == "Rope Connected"), "Has rope connected after using it with rope.");
        var actions = weapon.GetAvailableActions();
        Assert.IsTrue(actions.Any(x => x == "Untie"), "The untie action is available.");
        weapon.Act("Untie");
        Assert.IsFalse(actions.Any(x => x == "Untie"), "The untie action is no longer available.");
        Assert.IsFalse(weapon.Properties.Any(x => x.Type.Name == "Rope Connected"), "No longer has rope connected after using the untie action.");
    }

    [TestMethod]
    public void Item_Act_ThrowRemovesThrowableAndAddsThrownProperty()
    {
        var weapon = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        Assert.IsTrue(weapon.Properties.Any(x => x.Type.Name == "Throwable"));
        Assert.IsFalse(weapon.Properties.Any(x => x.Type.Name == "Thrown"));
        weapon.Act("Throw");
        Assert.IsFalse(weapon.Properties.Any(x => x.Type.Name == "Throwable"));
        Assert.IsTrue(weapon.Properties.Any(x => x.Type.Name == "Thrown"));
    }

    [TestMethod]
    public void Item_Act_ThrowTurnsThrowableIntoThrownAndPullRopeTurnsThrownIntoThrowable()
    {
        var dagger = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));

        // Connect the rope to the dagger.
        var rope = new Item(ItemManager.ItemTypes.First(x => x.Name == "Rope"));
        Assert.IsFalse(dagger.Properties.Any(x => x.Type.Name == "Rope Connected"), "Dagger does not start with a rope connected.");
        dagger.UseWith(rope);
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Rope Connected"), "Dagger has rope connected after being used with rope.");

        // Throw the dagger.
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Throwable"), "The dagger starts with the Throwable property.");
        var actions = dagger.GetAvailableActions();
        Assert.IsTrue(actions.Any(x => x == "Throw"), "The dagger has the Throw action available.");
        Assert.IsFalse(actions.Any(x => x == "Pull Rope"), "The dagger should not have the Pull Rope action avialable if not thrown.");
        dagger.Act("Throw");
        Assert.IsFalse(dagger.Properties.Any(x => x.Type.Name == "Throwable"), "The dagger should no longer have the Throwable property.");
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Thrown"), "The dagger should have the Thrown property.");
        actions = dagger.GetAvailableActions();
        Assert.IsTrue(actions.Any(x => x == "Pull Rope"), "The Pull Rope action should be available after throwing the rope connected dagger.");

        // Pull the rope.
        dagger.Act("Pull Rope");
        actions = dagger.GetAvailableActions();
        Assert.IsFalse(actions.Any(x => x == "Pull Rope"), "After pulling the rope the dagger should no longer have the Pull Rope action available.");
        Assert.IsTrue(actions.Any(x => x == "Throw"), "After pulling the rope the dagger should have the Throw action available.");
        Assert.IsFalse(dagger.Properties.Any(x => x.Type.Name == "Thrown"), "After pulling the rope the dagger should no longer have the Thrown property.");
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Throwable"), "After pulling the rope the dagger should have the Throwable property.");
    }

    [TestMethod]
    public void Item_Act_ThrownWeaponCannotBeUsedToAttack()
    {
        var dagger = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Weapon"), "Dagger starts with Weapon property.");
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Throwable"), "Dagger starts with Throwable property.");

        var actions = dagger.GetAvailableActions();
        Assert.IsTrue(actions.Any(x => x == "Attack"));
        Assert.IsTrue(actions.Any(x => x == "Throw"));

        dagger.Act("Throw");

        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Weapon"), "Dagger still has the Weapon property after being thrown.");
        Assert.IsFalse(dagger.Properties.Any(x => x.Type.Name == "Throwable"), "Dagger no longer has the Throwable property after being thrown.");
        Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Thrown"), "Dagger has the Thrown property after being thrown.");

        actions = dagger.GetAvailableActions();
        Assert.IsFalse(actions.Any(x => x == "Attack"), "Dagger no longer has the Attack action after being thrown.");
        Assert.IsFalse(actions.Any(x => x == "Throw"), "Dagger no longer has the Throw action after being thrown.");
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenInteractionNotFound()
    {
        var sourceItem = new Item(new ItemType("A", "a"));
        var targetItem = new Item(new ItemType("B", "b"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(targetItem);
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenPropertyNotFound()
    {
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
        Assert.IsFalse(sourceItem.Properties.Any(x => x.Type.Name == "Rope Connected"), "Source item does not have rope connected to start.");
        sourceItem.UseWith(targetItem);
        Assert.IsTrue(sourceItem.Properties.Any(x => x.Type.Name == "Rope Connected"), "Source item does not have rope connected to start.");
    }

    [TestMethod]
    public void Item_UseWith_AddsPropertyWithValue_WhenInteractionAndPropertyFound()
    {
        var sourceItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var targetItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        Assert.IsFalse(sourceItem.Properties.Any(x => x.Type.Name == "Poisoned"), "Source item did not start with the Poisoned property.");
        sourceItem.UseWith(targetItem);
        Assert.IsTrue(sourceItem.Properties.Any(x => x.Type.Name == "Poisoned"), "Source item has the Poisoned property after being used with Poison Vial.");
        var property = sourceItem.Properties.FirstOrDefault(x => x.Type.Name == "Poisoned");
        Assert.IsTrue(property.Attributes.Any(x => x.Type.Name == "Number of Uses"));
        Assert.AreEqual<int>(1, property.Attributes.First(x => x.Type.Name == "Number of Uses").AttributeValue);
        Assert.IsTrue(property.Attributes.Any(x => x.Type.Name == "Added Poison Damage"));
        Assert.AreEqual<int>(10, property.Attributes.First(x => x.Type.Name == "Added Poison Damage").AttributeValue);
    }

    [TestMethod]
    public void Item_UseWith_RemovesProperty_WhenInteractionAndPropertyFound()
    {
        var sourceItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var targetItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Rope"));
        Assert.IsFalse(sourceItem.Properties.Any(x => x.Type.Name == "Rope Connected"), "Source item does not have rope connected to start.");
        sourceItem.UseWith(targetItem);
        Assert.IsTrue(sourceItem.Properties.Any(x => x.Type.Name == "Rope Connected"), "Source item does not have rope connected to start.");

        var property = sourceItem.Properties.FirstOrDefault(x => x.Type.Name == "Rope Connected");
        Assert.IsNotNull(property);

        var anotherTargetItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        sourceItem.UseWith(anotherTargetItem);
        Assert.IsFalse(sourceItem.Properties.Any(x => x.Type.Name == "Rope Connected"), "Source item does not have rope connected to start.");
    }

    [TestMethod]
    public void Item_Use_TriggersItemUsedEvent()
    {
        var eventsTriggered = new List<ItemEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        item.Used += delegate (object? sender, ItemEventArgs e)
        {
            eventsTriggered.Add(e);
        };

        item.Use();
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
        Assert.AreEqual<Item>(item, eventsTriggered.First().Item);
    }

    [TestMethod]
    public void Item_Use_TriggersPropertyItemUsedEvent()
    {
        var eventsTriggered = new List<ItemEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);

        var itemProperty = item.Properties.First(x => x.Type.Name == "Poisoned");
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
        var itemProperty = item.Properties.First(x => x.Type.Name == "Poisoned");
        var attribute = itemProperty.Attributes.First(x => x.Type.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.Type.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
    }

    [TestMethod]
    public void Item_Use_AttributeFiresExpiredEventWhenValueReachesZero()
    {
        var eventsTriggered = new List<AttributeExpiredEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);
        var itemProperty = item.Properties.First(x => x.Type.Name == "Poisoned");
        var attribute = itemProperty.Attributes.First(x => x.Type.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.Type.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        Assert.AreEqual<bool>(true, attribute.Type.IsRemovedWhenValueReachesZero, "Check that 'Number of Uses' is configured to expire when zero.");
        attribute.AttributeExpired += delegate (object? sender, AttributeExpiredEventArgs e)
        {
            eventsTriggered.Add(e);
        };
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
    }

    [TestMethod]
    public void Item_Use_PropertyExpiresWhenAttributeExpires()
    {
        var eventsTriggered = new List<PropertyExpiredEventArgs>();

        var item = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
        var otherItem = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
        item.UseWith(otherItem);
        var itemProperty = item.Properties.First(x => x.Type.Name == "Poisoned");
        Assert.AreEqual<bool>(true, itemProperty.Type.WillExpireWhenAnAttributeExpires, "Check that 'Poisoned' is confiugred to expire when an attribute expires.");
        itemProperty.HasExpired += delegate (object? sender, PropertyExpiredEventArgs e)
        {
            eventsTriggered.Add(e);
        };
        var attribute = itemProperty.Attributes.First(x => x.Type.Name == "Number of Uses");
        Assert.AreEqual<int>(1, attribute.AttributeValue, "Starting number of uses.");
        Assert.AreEqual<bool>(true, attribute.Type.WillValueDecreaseOnUse, "Check that 'Number of Uses' is configured to reduce on use.");
        Assert.AreEqual<bool>(true, attribute.Type.IsRemovedWhenValueReachesZero, "Check that 'Number of Uses' is configured to expire when zero.");
        item.Use();
        Assert.AreEqual<int>(0, attribute.AttributeValue, "Number of uses after being used.");
        Assert.AreEqual<int>(1, eventsTriggered.Count, "Number of events triggered.");
    }
}
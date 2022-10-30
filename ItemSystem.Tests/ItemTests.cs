using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class ItemTests
{
    [TestMethod]
    public void Item_Constructor()
    {
        var myDagger = new Item(new ItemType("Dagger", "a small blade with a small handle"));
        Assert.IsNotNull(myDagger);
        Assert.IsNotNull(myDagger.Type);
    }

    [TestMethod]
    public void Item_ItemProperty_Descriptions()
    {
        var myDagger = new Item(new ItemType("Dagger", "a small blade with a small handle"));

        Assert.IsNotNull(myDagger);
        Assert.IsNotNull(myDagger.Type);
        Assert.AreEqual<string>("a small blade with a small handle", myDagger.Type.Description);
        Assert.AreEqual<string>("A small blade with a small handle.", myDagger.Description);

        myDagger.Properties.Add(new ItemProperty("Rope", "connected to a rope"));

        Assert.IsNotNull(myDagger.Properties);
        Assert.AreEqual<string>("A small blade with a small handle connected to a rope.", myDagger.Description);
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenInteractionNotFound()
    {
        var game = new GameManager();
        var sourceItem = new Item(new ItemType("A", "a"));
        var targetItem = new Item(new ItemType("B", "b"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(game, targetItem);
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
    }

    [TestMethod]
    public void Item_UseWith_DoesNothing_WhenItemPropertyNotFound()
    {
        var game = new GameManager();
        var sourceItem = new Item(new ItemType("B", "b"));
        var targetItem = new Item(new ItemType("C", "c"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(game, targetItem);
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
    }

    [TestMethod]
    public void Item_UseWith_AddsProperty_WhenInteractionAndPropertyFound()
    {
        var game = new GameManager();
        var sourceItem = new Item(game.ItemTypes.First(x => x.Name == "Dagger"));
        var targetItem = new Item(game.ItemTypes.First(x => x.Name == "Rope"));
        Assert.AreEqual<int>(0, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        sourceItem.UseWith(game, targetItem);
        Assert.AreEqual<int>(1, sourceItem.Properties.Count);
        Assert.AreEqual<int>(0, targetItem.Properties.Count);
        var property = sourceItem.Properties.FirstOrDefault(x => x.Name == "Rope Connected");
        Assert.IsNotNull(property);
    }
}
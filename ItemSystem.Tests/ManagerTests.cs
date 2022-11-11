using ItemSystem.Instances;
using ItemSystem.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class ManagerTests
{
    [TestMethod]
    public void Manager_Constructor()
    {
        var itemTypeManager = new Manager<ItemType>();
        var itemPropertyManager = new Manager<Property>();
    }
    [TestMethod]
    public void Manager_Load_ItemTypes()
    {
        var manager = new Manager<ItemType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");
        var results = manager.ToList();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_ItemPropertyTypes()
    {
        var manager = new Manager<PropertyType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyTypes.json");
        var results = manager.ToList();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_ItemInteractionTypes()
    {
        var manager = new Manager<InteractionType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");
        var results = manager.ToList();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_Clear()
    {
        var manager = new Manager<PropertyType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemPropertyTypes.json");
        var results = manager.ToList();
        Assert.IsNotNull(results);
        Assert.AreNotEqual(0, results.Count());
        manager.Clear();
        Assert.IsNotNull(results);
        Assert.AreEqual<int>(0, manager.Count());
    }
}
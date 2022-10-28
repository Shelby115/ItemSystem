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
        var itemPropertyManager = new Manager<ItemProperty>();
    }
    [TestMethod]
    public void Manager_Load_ItemTypes()
    {
        var manager = new Manager<ItemType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemTypes.json");
        var results = manager.Get();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_ItemProperties()
    {
        var manager = new Manager<ItemProperty>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemProperties.json");
        var results = manager.Get();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_ItemInteractionTypes()
    {
        var manager = new Manager<ItemInteractionType>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemInteractionTypes.json");
        var results = manager.Get();
        Assert.IsNotNull(results);
    }

    [TestMethod]
    public void Manager_Load_Clear()
    {
        var manager = new Manager<ItemProperty>();
        manager.Load($"{Directory.GetCurrentDirectory()}../../../../../ItemProperties.json");
        var results = manager.Get();
        Assert.IsNotNull(results);
        Assert.AreNotEqual(0, results.Count());
        manager.Clear();
        Assert.IsNotNull(results);
        Assert.AreEqual<int>(0, manager.Get().Count());
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class ItemManagerTests
{
    [TestMethod]
    public void ItemManager_Constructor()
    {
        Assert.IsNotNull(ItemManager.ItemTypes);
        Assert.AreNotEqual<int>(0, ItemManager.ItemTypes.Count());

        Assert.IsNotNull(ItemManager.PropertyTypes);
        Assert.AreNotEqual<int>(0, ItemManager.PropertyTypes.Count());

        Assert.IsNotNull(ItemManager.InteractionTypes);
        Assert.AreNotEqual<int>(0, ItemManager.InteractionTypes.Count());

        Assert.IsNotNull(ItemManager.AttributeTypes);
        Assert.AreNotEqual<int>(0, ItemManager.AttributeTypes.Count());

        Assert.IsNotNull(ItemManager.InnateItemPropertyTypes);
        Assert.AreNotEqual<int>(0, ItemManager.InnateItemPropertyTypes.Count());

        Assert.IsNotNull(ItemManager.PropertyActionTypes);
        Assert.AreNotEqual<int>(0, ItemManager.PropertyActionTypes.Count());
    }
}
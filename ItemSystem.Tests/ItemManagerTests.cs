using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class ItemManagerTests
{
    [TestMethod]
    public void GameManager_Constructor()
    {
        Assert.IsNotNull(ItemManager.Types);
        Assert.AreNotEqual<int>(0, ItemManager.Types.Count());

        Assert.IsNotNull(ItemManager.Properties);
        Assert.AreNotEqual<int>(0, ItemManager.Properties.Count());

        Assert.IsNotNull(ItemManager.InteractionTypes);
        Assert.AreNotEqual<int>(0, ItemManager.InteractionTypes.Count());

        Assert.IsNotNull(ItemManager.AttributeTypes);
        Assert.AreNotEqual<int>(0, ItemManager.AttributeTypes.Count());

        Assert.IsNotNull(ItemManager.Attributes);
        Assert.AreNotEqual<int>(0, ItemManager.Attributes.Count());
    }
}
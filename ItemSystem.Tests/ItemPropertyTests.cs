using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemSystem.Tests;

[TestClass]
public class ItemPropertyTests
{
    [TestMethod]
    public void ItemProperty_Constructor()
    {
        var itemProperty = new ItemProperty("Rope", "connected to a rope");
        Assert.IsNotNull(itemProperty);
        Assert.IsNotNull(itemProperty.Name);
        Assert.IsNotNull(itemProperty.Description);
    }
}
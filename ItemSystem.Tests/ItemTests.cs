using Microsoft.VisualStudio.TestTools.UnitTesting;

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
}
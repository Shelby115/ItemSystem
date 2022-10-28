using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemSystem.Tests;

[TestClass]
public class ItemTypeTests
{
    [TestMethod]
    public void ItemType_Constructor()
    {
        var dagger = new ItemType("Dagger", "A small blade with a small handle.");
        var getterTest = $"A {dagger.Name.ToLower()} is {dagger.Description.ToLower()}";
    }
}
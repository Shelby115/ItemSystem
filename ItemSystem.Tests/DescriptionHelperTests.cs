using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ItemSystem.Tests;

[TestClass]
public class DescriptionHelperTests
{
    [TestMethod]
    public void Sentence_ReturnsEmptyString_WhenNull()
    {
        string description = null;
        Assert.AreEqual<string>("", description.Sentence());
    }

    [TestMethod]
    public void Sentence_ReturnsEmptyString_WhenWhiteSpace()
    {
        var description = " ";
        Assert.AreEqual<string>("", description.Sentence());
    }

    [TestMethod]
    public void Sentence_ReturnsSentence_WhenNotEmpty()
    {
        var description = " a small blade with a small handle       ";
        Assert.AreEqual<string>("A small blade with a small handle.", description.Sentence());
    }
}
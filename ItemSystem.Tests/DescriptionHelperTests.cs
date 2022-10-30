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

    [TestMethod]
    public void Sentence_ReturnsSentence_WhenASentenceIsSentenced()
    {
        var sentence = "A small blade with a small handle.";
        Assert.AreEqual<string>(sentence, sentence.Sentence());
        Assert.AreEqual<string>(sentence, sentence.Sentence().Unsentence().Sentence());
    }

    [TestMethod]
    public void Sentence_ReturnsSentence_WhenAnExclamationPointSentenceIsSentenced()
    {
        var sentence = "A small blade with a small handle!";
        Assert.AreEqual<string>(sentence, sentence.Sentence('!'));
        Assert.AreEqual<string>(sentence, sentence.Sentence('!').Unsentence().Sentence('!'));
    }

    [TestMethod]
    public void Sentence_ReturnsSentence_WhenAQuestionMarkSentenceIsSentenced()
    {
        var sentence = "A small blade with a small handle?";
        Assert.AreEqual<string>(sentence, sentence.Sentence('?'));
        Assert.AreEqual<string>(sentence, sentence.Sentence('?').Unsentence().Sentence('?'));
    }

    [TestMethod]
    public void Unsentence_ReturnsEmptyString_WhenNull()
    {
        string description = null;
        Assert.AreEqual<string>("", description.Unsentence());
    }

    [TestMethod]
    public void Unsentence_ReturnsEmptyString_WhenWhiteSpace()
    {
        var description = " ";
        Assert.AreEqual<string>("", description.Unsentence());
    }

    [TestMethod]
    public void Unsentence_ReturnsUnsentence_WhenNotEmpty()
    {
        var description = " A small blade with a small handle.    ";
        Assert.AreEqual<string>("a small blade with a small handle", description.Unsentence());
    }

    [TestMethod]
    public void Unsentence_ReturnsUnsentence_WhenExclamationPoint()
    {
        var description = " A small blade with a small handle!    ";
        Assert.AreEqual<string>("a small blade with a small handle", description.Unsentence());
    }

    [TestMethod]
    public void Unsentence_ReturnsUnsentence_WhenQuestionMark()
    {
        var description = " A small blade with a small handle?    ";
        Assert.AreEqual<string>("a small blade with a small handle", description.Unsentence());
    }

    [TestMethod]
    public void Unsentence_ReturnsUnsentence_WhenANonSentenceUnsentenced()
    {
        var nonSentence = "a small blade with a small handle";
        Assert.AreEqual<string>(nonSentence, nonSentence.Unsentence());
        Assert.AreEqual<string>(nonSentence, nonSentence.Unsentence().Sentence().Unsentence());
    }
}
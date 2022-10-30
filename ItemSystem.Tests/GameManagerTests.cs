using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ItemSystem.Tests;

[TestClass]
public class GameManagerTests
{
    [TestMethod]
    public void GameManager_Constructor()
    {
        var game = new GameManager();

        Assert.IsNotNull(game.ItemTypes);
        Assert.AreNotEqual<int>(0, game.ItemTypes.Count());

        Assert.IsNotNull(game.ItemProperties);
        Assert.AreNotEqual<int>(0, game.ItemProperties.Count());

        Assert.IsNotNull(game.ItemInteractionTypes);
        Assert.AreNotEqual<int>(0, game.ItemInteractionTypes.Count());
    }
}
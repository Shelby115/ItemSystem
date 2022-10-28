﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        Assert.AreNotEqual<int>(0, game.ItemTypes.Get().Count());

        Assert.IsNotNull(game.ItemProperties);
        Assert.AreNotEqual<int>(0, game.ItemProperties.Get().Count());

        Assert.IsNotNull(game.ItemInteractionTypes);
        Assert.AreNotEqual<int>(0, game.ItemInteractionTypes.Get().Count());
    }
}
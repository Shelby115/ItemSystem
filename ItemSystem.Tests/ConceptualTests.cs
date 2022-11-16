using ItemSystem.Instances;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemSystem.Tests
{
    /// <summary>
    /// The idea behind these conceptual tests it to represent examples of how this system could be used in a game.
    /// I'm going to try not to be too in-depth mechanically in these tests (e.g, checking for actions instead of properties).
    /// </summary>
    [TestClass]
    public class ConceptualTests
    {
        [TestMethod]
        public void Conceptual_RopeDagger_Weapon()
        {
            // Create the Rope Dagger weapon by using the dagger with the rope.
            var dagger = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));
            var rope = new Item(ItemManager.ItemTypes.First(x => x.Name == "Rope"));
            dagger.UseWith(rope);

            // Attack should be an available action by default.
            // Attacking does not remove the ability to attack.
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Attack"));
            dagger.Act("Attack");
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Attack"));

            // Throw should be an available action by default.
            // Throwing removes the ability to throw or attack, since it is no longer in the user's possession.
            // Having thrown the rope dagger should have made the Pull Rope action available.
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Throw"));
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Pull Rope"));
            dagger.Act("Throw");
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Attack"));
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Throw"));
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Pull Rope"));

            // Pulling the rope should make the Attack and Throw actions available again.
            dagger.Act("Pull Rope");
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Attack"));
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Throw"));

            // Having used the rope with the dagger should have made the Untie action available.
            // Using the Untie action should remove the Pull Rope action once the dagger is thrown.
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Untie"));
            dagger.Act("Untie");
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Untie"));
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Pull Rope"));
            dagger.Act("Throw");
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Throw"));
            Assert.IsFalse(dagger.GetAvailableActions().Any(x => x == "Pull Rope"));
        }
    }
}
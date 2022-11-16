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

        [TestMethod]
        public void Conceptual_PoisonedDagger_Weapon()
        {
            var dagger = new Item(ItemManager.ItemTypes.First(x => x.Name == "Dagger"));

            // The dagger should have the Attack and Throw actions available by default.
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Attack"));
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Throw"));

            // The dagger should not be poisoned by default.
            Assert.IsFalse(dagger.Properties.Any(x => x.Type.Name == "Poisoned"));

            // Using the poison vial with the dagger should make it poisoned.
            var poison = new Item(ItemManager.ItemTypes.First(x => x.Name == "Poison Vial"));
            dagger.UseWith(poison);
            Assert.IsTrue(dagger.Properties.Any(x => x.Type.Name == "Poisoned"));

            // Attack and Throw should still be available once the dagger has been poisoned.
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Attack"));
            Assert.IsTrue(dagger.GetAvailableActions().Any(x => x == "Throw"));

            // TODO: There should be some sort of system for getting action relevant details.
            // For example,
            //  Attacking with a normal dagger should give us weapon damage information.
            //  Attacking with a poinson dagger should give us weapon damage + poison damage information.

            // TODO: The Use() item property perhaps should be reconsidered.
            // The original intent was that Use() would use the item (e.g., Drink a healing potion, attack with a weapon, etc).
            //  However, this is less flexible than the Act() function would be, where we can have a "Drink" action for a potion and an "Attack" action for a weapon.
            //  Perhaps the best course here is to absolve the Use() function into a Use action and have the action system handle property and attribute interactions.
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSteve;

namespace Tests
{
    [TestClass]
    public class CharacterCreation
    {
        [TestMethod]
        public void ClassAssignment()
        {
            var cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.BARBARIAN, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.BARBARIAN);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.BARD, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.BARD);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.CLERIC, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.CLERIC);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.DRUID, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.DRUID);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.FIGHTER, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.FIGHTER);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.MONK, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.MONK);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.PALADIN);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.RANGER, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.RANGER);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.ROGUE, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.ROGUE);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.SORCERER, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.SORCERER);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.WARLOCK, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.WARLOCK);

            cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.WIZARD, true);
            Assert.AreEqual(cs.ClassInstance.Type, KnownValues.ClassType.WIZARD);
        }
    }
}

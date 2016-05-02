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

        [TestMethod]
        public void RaceAssigmnet()
        {
            var cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.DRAGONBORN, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.DRAGONBORN);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.DWARF, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.DWARF);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.ELF, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.ELF);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.GNOME, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.GNOME);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.HALF_ELF, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.HALF_ELF);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.HALFLING, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.HALFLING);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.HALF_ORC, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.HALF_ORC);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.HUMAN, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.HUMAN);

            cs = new CharacterSheet();
            cs.SetRace(KnownValues.Race.TIEFLING, true);
            Assert.AreEqual(cs.RaceInstance.Race, KnownValues.Race.TIEFLING);
        }
    }
}

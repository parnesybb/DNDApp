using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSteve;
using System.Xml;
using System.Xml.Serialization;

namespace Tests
{
    [TestClass]
    public class CharacterSheetTest
    {
        [TestMethod]
        public void CharacterSheetTest1()
        {
            CharacterSheet cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            cs.SetRace(KnownValues.Race.DWARF, true);
            cs.Background = KnownValues.Background.SAGE;

            var s = new XmlSerializer(typeof(CharacterSheet));
            var settings = new XmlWriterSettings();
            settings.Indent = true;

            using (var r = XmlWriter.Create("charSheetTest.xml", settings))
            {
                s.Serialize(r, cs);
            }
        }
    }
}

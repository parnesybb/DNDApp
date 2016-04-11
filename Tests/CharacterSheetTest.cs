using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoSteve;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Tests
{
    [TestClass]
    public class CharacterSheetSerializeDesializeTest
    {
        [TestMethod]
        public void CharacterSheetSerializeDesializeTest1()
        {
            CharacterSheet cs = new CharacterSheet();
            cs.SetClass(KnownValues.ClassType.PALADIN, true);
            cs.SetRace(KnownValues.Race.DWARF, true);
            cs.Background = KnownValues.Background.SAGE;

            var fs = new System.IO.FileStream("CS_OUT.dat", System.IO.FileMode.Create);
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, cs);
            fs.Close();

            fs = new System.IO.FileStream("CS_OUT.dat", System.IO.FileMode.Open);
            var dcs = formatter.Deserialize(fs);
            fs.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GoSteve.Structures.Classes
{
    [Serializable]
    public class Druid : AClass
    {
        public Druid()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;

            _classType = KnownValues.ClassType.DRUID;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a wooden shield or(b) any simple weapon");
            ret.Add("(a) a scimitar or(b) any simple melee weapon");
            ret.Add("Leather armor");
            ret.Add("explorer's pack");
            ret.Add("Druidic focus");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 8 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light armor - non metal");
            ret.Add("Medium armor - non metal");
            ret.Add("Shields - non metal");
            ret.Add("Clubs, daggers, darts, javelins, maces, quaterstaffs, scimitars, sickles, slings, spears");
            ret.Add("Herbalism Kit");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Druidic");
            ret.Add("Spellcasting");

            return ret.ToArray();
        }
    }
}
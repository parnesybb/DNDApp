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
    public class Wizard : AClass
    {
        public Wizard()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 6;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a quarterstaff or (b)a dagger");
            ret.Add("(a) a eomponent poueh or(b) an areane foeus");
            ret.Add("(a) a seholar's paek or (b) an explorer's paek"):
            ret.Add("A spellbook");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 6 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Daggers");
            ret.Add("Darts");
            ret.Add("Slings");
            ret.Add("Quaterstaffs");
            ret.Add("Light Crossbows");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Spellcasting");
            ret.Add("Arcane Recovery");

            return ret.ToArray();
        }
    }
}
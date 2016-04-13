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
    public class Sorcerer : AClass
    {
        public Sorcerer()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 6;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a light crossbow and 20 bolts or (b)any simple weapon");
            ret.Add("(a) a component pouch or(b) an arcane focus");
            ret.Add("(a) a dungeoneer's pack or (b) an explorer's pack");
            ret.Add("Two daggers");

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
            ret.Add("Quarterstaffs");
            ret.Add("Light Crossbows");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Spellcasting");
            ret.Add("Sorcerous Origin");

            return ret.ToArray();
        }
    }
}
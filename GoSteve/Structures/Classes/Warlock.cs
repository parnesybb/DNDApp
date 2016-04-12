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
    public class Warlock : AClass
    {
        public Warlock()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a lighl crossbow and 20 bo!ls or(b) any simple weapon");
            ret.Add("(a) a componenl pouch or (b)an arcane focus");
            ret.Add("(a) a scholar's pack or (b) a dungeoneer's pack");
            ret.Add("Leather armor, any simple weapon, and two daggers");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 8 + modifier;
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            throw new NotImplementedException();
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light Armor");
            ret.Add("Simple Weapons");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Otherwordly Patron");
            ret.Add("Pact Magic");

            return ret.ToArray();
        }
    }
}
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
    public class Fighter : AClass
    {
        public Fighter()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 10;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)chain mail or (b)leather, longbow, and 20 arrows");
            ret.Add("(a) a martial weapon and a shield or(b) two martial weapons");
            ret.Add("(a) a Iight crossbow and 20 bolts or(b) two handaxes");
            ret.Add("(a) a dungeoneer's pack or (b) an explorer's pack");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 10 + modifier;
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            throw new NotImplementedException();
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("All Armor");
            ret.Add("Shields");
            ret.Add("Simple Weapons");
            ret.Add("Martial Weapons");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Fighting Style");
            ret.Add("Second Wind");

            return ret.ToArray();
        }
    }
}
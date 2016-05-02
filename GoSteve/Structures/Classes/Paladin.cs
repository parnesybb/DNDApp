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
    public class Paladin : AClass
    {
        public Paladin()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 10;

            _classType = KnownValues.ClassType.PALADIN;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("Chain Mail");
            ret.Add("Holy Symbol");
            ret.Add("(a)a martial weapon and a shield OR (b)two martial weapons");
            ret.Add("(a) five javelins OR (b)any simple melee weapon");
            ret.Add("(a) a priest's pack OR (b)an explorer's pack");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 10 + modifier;
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

            ret.Add("Divine Sense");
            ret.Add("Lay on Hands");

            return ret.ToArray();
        }
    }
}
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
    public class Bloodhunter : AClass
    {
        public Bloodhunter()
        {
            this._classType = KnownValues.ClassType.BLOODHUNTER;
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 10;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a martial weapon or (b)two simple weapons");
            ret.Add("(a)a light crossbow and 20 bolts or (b)hand crossbow and 20 bolts");
            ret.Add("An expiorer's pack");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 10 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light Armor");
            ret.Add("Medium Armor");
            ret.Add("Simple Weapons");
            ret.Add("Martial Weapons");
            ret.Add("Alchemist's Supplies");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<String>();

            ret.Add("Hunter's Bane");
            ret.Add("Crimson Rite");

            return ret.ToArray();
        }
    }
}
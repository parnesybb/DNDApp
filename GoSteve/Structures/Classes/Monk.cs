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
    public class Monk : AClass
    {
        public Monk()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a shortsword or (b)any simple weapon");
            ret.Add("(a) a dungeoneer's pack or (b) an explorer's pack");
            ret.Add("10 darts");

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

            ret.Add("Simple Weapons");
            ret.Add("Shortswords");
            ret.Add("1 artisan's tool or one musical instrument");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Unarmored Defense");
            ret.Add("Martial Arts");

            return ret.ToArray();
        }
    }
}
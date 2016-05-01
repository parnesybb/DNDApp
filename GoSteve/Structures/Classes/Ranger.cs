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
    public class Ranger : AClass
    {
        public Ranger()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 10;
            _classType = KnownValues.ClassType.RANGER;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)scalemail or(b) leather armor");
            ret.Add("(a)two shortswords or(b) two sim pie melee weapons");
            ret.Add("(a) a dungeoneer's pack or (b) an explorer's pack");
            ret.Add("A longbow and a quiver of 20 arrows");

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
            ret.Add("Shields");
            ret.Add("Simple Weapons");
            ret.Add("Martial Weapons");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Favored Enemy");
            ret.Add("Natural Explorer");

            return ret.ToArray();
        }
    }
}
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
    public class Rogue : AClass
    {
        public Rogue()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a rapier or (b)a shortsword");
            ret.Add("(a) a shortbow and quiver of 20 arrows or(b) a shortsword");
            ret.Add("(a) a burglar's pack, (b) a dungeoneer's pack, or(c) an explorer's pack");
            ret.Add("Leather armor");
            ret.Add("two daggers");
            ret.Add("thieves' tools");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 8 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light Armor");
            ret.Add("Simple Weapons");
            ret.Add("Hand Crossbows");
            ret.Add("Longswords");
            ret.Add("Rapiers");
            ret.Add("Shortswords");
            ret.Add("Thieves' Tools");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Expertise");
            ret.Add("Sneak Attack");
            ret.Add("Thieves' Cant");

            return ret.ToArray();
        }
    }
}
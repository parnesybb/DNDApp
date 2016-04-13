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
    public class Bard : AClass
    {
        public Bard()
        {
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 8;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a rapier, (b)a longsword, or(c) any simple weapon");
            ret.Add("(a) a diplomat's pack or (b) an entertainer's pack");
            ret.Add("(a)a lute or(b) any other musical instrument");
            ret.Add("Leather armor");
            ret.Add("Dagger");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 8 + modifier;
        }

        public override string[] GetProficiencies()
        {
            var ret = new List<string>();

            ret.Add("Light armor");
            ret.Add("Simple Weapons");
            ret.Add("Hand Crossbows");
            ret.Add("Longswords");
            ret.Add("Rapiers");
            ret.Add("Shortswords");
            ret.Add("Pick 3 musical instruments.");

            return ret.ToArray();
        }

        public override string[] GetTraits()
        {
            var ret = new List<string>();

            ret.Add("Spellcasting");
            ret.Add("Bardic Inspiration (d6)");
            ret.Add("Jack of All Trades");
            ret.Add("Song of Rest (d6)");

            return ret.ToArray();
        }
    }
}
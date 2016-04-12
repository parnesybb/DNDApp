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
    class Barbarian : AClass
    {   
        public Barbarian()
        {
            this._classType = KnownValues.ClassType.BARBARIAN;
            this.HitDice.TotalAmount = 1;
            this.HitDice.AvailableAmount = 1;
            this.HitDice.NumberOfSides = 12;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("(a)a greataxe or (b)any martial melee weapon");
            ret.Add("(a) two handaxes or(b) any simple weapon");
            ret.Add("An expiorer's pack and four javelins");

            return ret.ToArray();
        }

        public override int GetLevelOneHitPoints(int modifier)
        {
            return 12 + modifier;
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
            var ret = new List<String>();

            ret.Add("Rage");
            ret.Add("Unarmored Defense");

            return ret.ToArray();
        }
    }
}
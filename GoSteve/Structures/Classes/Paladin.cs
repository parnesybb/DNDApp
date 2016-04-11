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
            this._classType = KnownValues.ClassType.PALADIN;
            this._hitDice.TotalAmount = 1;
            this._hitDice.AvailableAmount = 1;
            this._hitDice.NumberOfSides = 10;
        }

        public override string[] GetEquipment()
        {
            var ret = new List<string>();

            ret.Add("Chain Mail");
            ret.Add("Holy Symbol");

            return ret.ToArray();
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
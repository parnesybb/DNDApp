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
    public class Paladin : AClass
    {
        public Paladin()
        {
            this._classType = KnownValues.ClassType.PALADIN;
            this._hitDice.TotalAmount = 1;
            this._hitDice.AvailableAmount = 1;
            this._hitDice.NumberOfSides = 10;

            this._traits.Add("Divine Sense");
            this._traits.Add("Lay on Hands");
            this._profs.Add("All Armor");
            this._profs.Add("Shields");
            this._profs.Add("Simple Weapons");
            this._profs.Add("Martial Weapons");
            this._equip.Add("Chain Mail");
            this._equip.Add("Holy Symbol");
        }
    }
}
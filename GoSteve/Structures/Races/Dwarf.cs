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

namespace GoSteve.Structures.Races
{
    [Serializable]
    public class Dwarf : ARace
    {
        public Dwarf()
        {
            this._race = KnownValues.Race.DWARF;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 25;
            this._size = ARace.SMALL_SIZE;
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if (value == KnownValues.SubRace.HILL_DWARF || value == KnownValues.SubRace.MOUNTAIN_DWARF)
                {
                   this._subRace = value;
                }
                else
                {
                    this._subRace = KnownValues.SubRace.NONE;
                }          
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Dark Vision - 60FT");
            ret.Add("Dwarven Resilience");
            ret.Add("Stonecunning");

            if (this.SubRace == KnownValues.SubRace.HILL_DWARF)
            {
                ret.Add("Dwarven Toughness");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Battleaxe");
            ret.Add("Handaxe");
            ret.Add("Throwing Hammer");
            ret.Add("Warhammer");
            ret.Add("Common");
            ret.Add("Dwarvish");

            if (this.SubRace == KnownValues.SubRace.MOUNTAIN_DWARF)
            {
                ret.Add("Light Armor");
                ret.Add("Medium Armor");
            }

            return ret.ToArray();
        }
    }
}
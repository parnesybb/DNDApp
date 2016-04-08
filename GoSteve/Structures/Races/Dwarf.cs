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
    public class Dwarf : ARace
    {
        public Dwarf()
        {
            this._race = KnownValues.Race.DWARF;
            this._speed = 25;
            this._featuresTraits.Add("Dark Vision - 60FT");
            this._featuresTraits.Add("Dwarven Resilience");
            this._featuresTraits.Add("Stonecunning");
            this._profsAndlanguages.Add("Battleaxe");
            this._profsAndlanguages.Add("Handaxe");
            this._profsAndlanguages.Add("Throwing Hammer");
            this._profsAndlanguages.Add("Warhammer");
            this._profsAndlanguages.Add("Common");
            this._profsAndlanguages.Add("Dwarvish");
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if (value == KnownValues.SubRace.HILL_DWARF)
                {
                    this._subRace = value;
                    this._featuresTraits.Add("Dwarven Toughness");
                }
                else if (value == KnownValues.SubRace.MOUNTAIN_DWARF)
                {
                    this._subRace = value;
                    this._profsAndlanguages.Add("Light Armor");
                    this._profsAndlanguages.Add("Medium Armor");
                }
                else
                {
                    this._subRace = KnownValues.SubRace.NONE;
                }
            }
        }
    }
}
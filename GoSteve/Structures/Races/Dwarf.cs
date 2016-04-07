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
        }

        public override KnownValues.Race Race
        {
            get
            {
                return this._race;
            }
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
                    this._subRace = value;
                else
                    this._subRace = KnownValues.SubRace.NONE;
            }
        }
    }
}
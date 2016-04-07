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
    public abstract class ARace
    {
        protected KnownValues.Race _race;
        protected KnownValues.SubRace _subRace;

        public ARace()
        {
            this._subRace = KnownValues.SubRace.NONE;
        }

        public abstract KnownValues.Race Race
        {
            get;
        }

        public abstract KnownValues.SubRace SubRace
        {
            get; set;
        }
    }
}
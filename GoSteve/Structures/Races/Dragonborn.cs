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
    class Dragonborn : ARace
    {
        public Dragonborn()
        {
            this._race = KnownValues.Race.DRAGONBORN;
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
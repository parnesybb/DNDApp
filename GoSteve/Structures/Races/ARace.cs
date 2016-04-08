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
        protected int _speed;
        protected List<string> _profsAndlanguages;
        protected List<string> _featuresTraits;

        public ARace()
        {
            this._subRace = KnownValues.SubRace.NONE;
            this._profsAndlanguages = new List<string>();
            this._featuresTraits = new List<string>();
            this._speed = 0;
        }

        public abstract KnownValues.SubRace SubRace
        {
            get; set;
        }

        public string[] ProficienciesLanguages
        {
            get
            {
                return this._profsAndlanguages.ToArray();
            }
        }

        public string[] FeaturesTraits
        {
            get
            {
                return this._featuresTraits.ToArray();
            }
        }

        public KnownValues.Race Race
        {
            get
            {
                return this._race;
            }
        }

        public int Speed
        {
            get
            {
                return this._speed;
            }
        }
    }
}
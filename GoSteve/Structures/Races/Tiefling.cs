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
    public class Tiefling : ARace
    {
        public Tiefling()
        {
            this._race = KnownValues.Race.TIEFLING;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        /// <summary>
        /// Not valid for the Tiefling race.
        /// </summary>
        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if(value == KnownValues.SubRace.ORIGINAL || value == KnownValues.SubRace.FERAL)
                {
                    this._subRace = value;
                }
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Dark Vision - 60ft");
            ret.Add("Hellish Resistance");
            ret.Add("Infernal Legacy or Devil's Tongue or Hellfire or Winged");

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Common");
            ret.Add("Infernal");

            return ret.ToArray();
        }
    }
}
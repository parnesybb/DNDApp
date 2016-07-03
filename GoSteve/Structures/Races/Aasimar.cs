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
    public class Aasimar : ARace
    {
        public Aasimar()
        {
            this._race = KnownValues.Race.AASIMAR;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        /// <summary>
        /// Not valid for Dragonborn.
        /// </summary>
        public override KnownValues.SubRace SubRace
        {
            get
            {
                return KnownValues.SubRace.NONE;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Darkvision");
            ret.Add("Celestial Resistance");
            ret.Add("Celestial Legacy");

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();
            ret.Add("Common");
            ret.Add("Celestial");

            return ret.ToArray();
        }
    }
}
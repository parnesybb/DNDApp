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
    public class Human : ARace
    {
        public Human()
        {
            this._race = KnownValues.Race.HUMAN;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        /// <summary>
        /// Not valid for the Human race.
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
            return new List<string>().ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();
            ret.Add("Common");
            return ret.ToArray();
        }
    }
}
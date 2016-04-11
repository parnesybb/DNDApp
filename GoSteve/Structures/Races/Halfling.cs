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
    public class Halfling : ARace
    {
        public Halfling()
        {
            this._race = KnownValues.Race.HALFLING;
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
                if (value == KnownValues.SubRace.LIGHTFOOT_HALFLING || value == KnownValues.SubRace.STOUT_HALFLING)
                {
                    this._subRace = value;
                }
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Lucky");
            ret.Add("Brave");
            ret.Add("Halfling Nimbleness");

            if (this._subRace == KnownValues.SubRace.LIGHTFOOT_HALFLING)
            {
                ret.Add("Naturally Stealthy");
            }
            else if (this._subRace == KnownValues.SubRace.STOUT_HALFLING)
            {
                ret.Add("Stout Resilience");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Common");
            ret.Add("Halfling");

            return ret.ToArray();
        }

        public override string[] GetFeaturesTraits()
        {
            throw new NotImplementedException();
        }

        public override string[] GetProficienciesLanguages()
        {
            throw new NotImplementedException();
        }
    }
}
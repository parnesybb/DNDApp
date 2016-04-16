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
    public class HalfElf : ARace
    {
        public HalfElf()
        {
            this._race = KnownValues.Race.HALF_ELF;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        /// <summary>
        /// Not valid for HalfElf race.
        /// </summary>
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

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            ret.Add("Dark Vision - 60ft");
            ret.Add("Fey Ancestry");

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Skill Versatility");
            ret.Add("Common");
            ret.Add("Elvish");
            ret.Add("CHOOSE ONE EXTRA LANGUAGE");

            return ret.ToArray();
        }
    }
}
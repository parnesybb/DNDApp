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
    public class Genasi : ARace
    {
        public Genasi()
        {
            this._race = KnownValues.Race.GENASI;
            this._subRace = KnownValues.SubRace.NONE;
            this._speed = 30;
            this._size = ARace.MEDIUM_SIZE;
        }

        public override KnownValues.SubRace SubRace
        {
            get
            {
                return this._subRace;
            }

            set
            {
                if (value == KnownValues.SubRace.AIR_GENASI || value == KnownValues.SubRace.EARTH_GENASI || value == KnownValues.SubRace.FIRE_GENASI || value == KnownValues.SubRace.WATER_GENASI)
                {
                    this._subRace = value;
                }
                else
                {
                    this._subRace = KnownValues.SubRace.NONE;
                }
            }
        }

        public override string[] GetFeaturesTraits()
        {
            var ret = new List<string>();

            if (this.SubRace == KnownValues.SubRace.AIR_GENASI)
            {
                ret.Add("Unending Breath");
                ret.Add("Mingle With the Wind");
            }
            else if(this.SubRace == KnownValues.SubRace.EARTH_GENASI)
            {
                ret.Add("Earth Walk");
                ret.Add("Merge With Stone");
            }
            else if(this.SubRace == KnownValues.SubRace.FIRE_GENASI)
            {
                ret.Add("Darkvision");
                ret.Add("Fire Resistance");
                ret.Add("Reach to the Blaze");
            }
            else if(this.SubRace == KnownValues.SubRace.WATER_GENASI)
            {
                ret.Add("Acid Resistance");
                ret.Add("Amphibious");
                ret.Add("Swim");
                ret.Add("Call to the Wave");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();

            ret.Add("Common");
            ret.Add("Primordial");

            return ret.ToArray();
        }
    }
}
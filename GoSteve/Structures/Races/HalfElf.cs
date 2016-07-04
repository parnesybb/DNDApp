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
                return this._subRace;
            }

            set
            {
                if(value == KnownValues.SubRace.ORIGINAL || value == KnownValues.SubRace.WOOD_ELF || value == KnownValues.SubRace.HIGH_ELF || value == KnownValues.SubRace.DROW || value == KnownValues.SubRace.WATER_ELF)
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

            ret.Add("Dark Vision - 60ft");
            ret.Add("Fey Ancestry");

            if(this.SubRace == KnownValues.SubRace.ORIGINAL)
            {
                ret.Add("Skill Versatility or Keen Senses");
            }
            else if(this.SubRace == KnownValues.SubRace.WOOD_ELF)
            {
                ret.Add("Elf Weapon Training or Fleet of Foot, or Mask of the Wild");
            }
            else if(this.SubRace == KnownValues.SubRace.HIGH_ELF)
            {
                ret.Add("Elf Weapon Training or High Elf Cantrip");
            }
            else if(this.SubRace == KnownValues.SubRace.DROW)
            {
                ret.Add("Drow Magic");
            }
            else if(this.SubRace == KnownValues.SubRace.WATER_ELF)
            {
                ret.Add("Swim speed - 30ft");
            }

            return ret.ToArray();
        }

        public override string[] GetProficienciesLanguages()
        {
            var ret = new List<string>();
            ret.Add("Common");
            ret.Add("Elvish");
            ret.Add("CHOOSE ONE EXTRA LANGUAGE");

            return ret.ToArray();
        }
    }
}